using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BBC_News_Classification.Extensions;
using BBC_News_Classification.Models;

namespace BBC_News_Classification.Cacheable
{
    public static class ActionCacheable
    {
        /// <summary>
        /// This method saves words in cache cleaning its punctuations and spaces
        /// </summary>
        /// <param name="words"></param>
        public static void UpdateWords(List<Word> words)
        {
       
            if (CacheableData.Words.Count > 0)
            {
                foreach (var word in words)
                {
                    word.Content = word?.Content?.StripPunctuation();
                    if (word?.Content != "")
                    {
                        var result = CacheableData.Words.Find(t => t.Content == word.Content);
                        if (result != null)
                        {
                            if (word.Category != result.Category)
                            {
                                if (word.Count > result.Count)
                                {
                                    result.Category = word.Category;
                                }
                            }
                            else
                            {
                                result.Count++;
                            }
                        }
                        else
                        {
                            CacheableData.Words.Add(word);
                        }
                    }
                }
            }
            else
            {
                CacheableData.Words.AddRange(words);
            }
        }

        /// <summary>
        /// This method sort words by category
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public static List<Word> SortingWordsReturnCopy(string category)
        {
            List<Word> words = CacheableData.Words.Where(t => t.Category == category).OrderByDescending(t => t.Count).ToList();
            return words;
        }

    }
}