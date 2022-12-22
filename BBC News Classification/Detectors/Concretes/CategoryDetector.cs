using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BBC_News_Classification.Cacheable;
using BBC_News_Classification.Detectors.Abstracts;
using BBC_News_Classification.Models;
using CsvHelper;

namespace BBC_News_Classification.Detectors.Concretes
{
    /// <summary>
    /// Detect usage categories in BBC News Classification DATA set
    /// </summary>
    public class CategoryDetector : ICategoryDetector
    {
        private readonly string _file;


        public CategoryDetector(string file)
        {
            _file = file;
        }

        /// <summary>
        /// After reads and maps to TrainingNews model from CSV file. Handled list group by categories and detects categories using LINQ expression
        /// Detected categories save dictionary as key and value using CacheableData class
        /// </summary>
        public void DetectCategories()
        {
            using (var reader = new StreamReader(_file))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<TrainingNews>();
                var trainingNewsEnumerable = records as TrainingNews[] ?? records.ToArray();
                var numberOfCategory = trainingNewsEnumerable.GroupBy(t => t.Category).Select(t => t.Count()).ToList();
                var group = trainingNewsEnumerable.GroupBy(t => t.Category).Select(t => t.Key).ToList();

                for (int i = 0; i < group.Count(); i++)
                {
                    CacheableData.NewsCategory.Add(group[i], numberOfCategory[i]);
                }
            }
        }
        /// <summary>
        /// get current categories
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var text = CacheableData.NewsCategory.Aggregate("", (current, kvp) => current + $"{kvp.Key},{kvp.Value}\n");
            return text;
        }
    }
}