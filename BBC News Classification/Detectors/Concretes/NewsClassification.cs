using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BBC_News_Classification.Aho_Corasick;
using BBC_News_Classification.Cacheable;
using BBC_News_Classification.Detectors.Abstracts;
using BBC_News_Classification.Models;
using CsvHelper;

namespace BBC_News_Classification.Detectors.Concretes
{
    public class NewsClassification : INewsClassification
    {
        private readonly string _file;
        private static List<Word> _usageKeyword = new List<Word>();

        public NewsClassification(string file)
        {
            _file = file;
        }

        /// <summary>
        /// Detect category in Test Data set
        /// </summary>
        public void DetectNewsCategory()
        {
            using (var reader = new StreamReader(_file))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var searchAlgorithm = new AhoCorasick();
                var records = csv.GetRecords<TestNews>();

                foreach (var record in records.ToList())
                {
                    ControlByCategory(record);
                }
            }
        }

        /// <summary>
        /// Control by words and 
        /// </summary>
        /// <param name="news"></param>
        /// <exception cref="ArgumentNullException"></exception>
        private void ControlByCategory(TestNews news)
        {
            foreach (var item in CacheableData.NewsCategory)
            {
                var category = item.Key;
                if (category != null)
                {
                    var words = ActionCacheable.SortingWordsReturnCopy(category);
                    if (news.Text != null) FindKeywordsInText(words, news.Text);
                }
                else
                {
                    throw new ArgumentNullException("category");
                }
            }

            var classifiedNews = NewsCategoryDecision(news);
            CacheableData.ClassifiedNews.Add(classifiedNews);
        }

        /// <summary>
        /// Find keywords in text. If there is 5 words belong to business category 2 words belong to sport we can say easily this news belongs to business category 
        /// </summary>
        /// <param name="keywords"></param>
        /// <param name="text"></param>
        private void FindKeywordsInText(List<Word> keywords, string text)
        {
            foreach (var keyword in keywords)
            {
                
                var result = text.Contains(keyword?.Content);
                if (result)
                {
                    var any = _usageKeyword.Any(t => t.Content.Equals(keyword?.Content) && t.Category == keyword.Category);
                    if(any != true) _usageKeyword.Add(keyword);
                }
            }
        }

        /// <summary>
        /// Detect how many words use in the text. And detects how many words belongs to which category
        /// </summary>
        /// <param name="test"></param>
        /// <returns></returns>
        private ClassifiedNews NewsCategoryDecision(TestNews test)
        {

            foreach (var item in _usageKeyword.ToList())
            {
                var usageList = _usageKeyword.Where(t => t.Content != item.Content && t.Category != item.Category);
                if (usageList.Any())
                {
                    var groupList = usageList.GroupBy(t => t.Category).Select((t, Index) => new CategoryUsage
                    {
                        Category = t.First().Category,
                        Count = t.Count(),
                        Index = Index
                    }).OrderByDescending(t => t.Count).ToList();
                    for (int i = groupList.Count - 1; i > 0; i--)
                    {
                        _usageKeyword.RemoveAt(groupList[i].Index);
                    }
                }
            }

            var sortedVariable = _usageKeyword.OrderByDescending(t => t.Count).ToList();


            return new ClassifiedNews
            {
                Category = sortedVariable.First().Category,
                Id = test.ArticleId,
                Text = test.Text
            };
        }
    }
}
