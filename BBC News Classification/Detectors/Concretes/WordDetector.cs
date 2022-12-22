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
using static System.Net.Mime.MediaTypeNames;

namespace BBC_News_Classification.Detectors.Concretes
{
    internal class WordDetector : IWordDetector
    {
        private readonly string _file;

        public WordDetector(string file)
        {
            _file = file;
        }

        /// <summary>
        /// When CSV training file reads the algorithm is trying to detect which category most use which word in the text.
        /// For instance If Business category use 789 times to 'Money' words more than other categories,
        /// according to Money word might belong to Business 
        /// </summary>
        public void DetectMostUsageWordByCategory()
        {
            using (var reader = new StreamReader(_file))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<TrainingNews>();
                var trainingNewsEnumerable = records as TrainingNews[] ?? records.ToArray();

                foreach (var category in CacheableData.NewsCategory)
                {
                    var news = trainingNewsEnumerable.ToList().Where(t => t.Category == category.Key);
                    foreach (var item in news)
                    {
                        var punctuation = item?.Text?.Where(Char.IsPunctuation).Distinct().ToArray();
                        var words = item?.Text?.Split().Select(x => x.Trim(punctuation)).ToList();
                        var group = words.GroupBy(t => t).Select(t => new Word
                        {
                            Count = t.Count(),
                            Content = t.Key,
                            Category = category.Key,
                        }).ToList();

                        ActionCacheable.UpdateWords(group);
                    }
                }
              
            }
        }
    }
}