using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BBC_News_Classification.Models;

namespace BBC_News_Classification.Cacheable
{
    public static class CacheableData
    {
        public static Dictionary<string?, int> NewsCategory = new();
        public static List<Word> Words = new();
        public static List<ClassifiedNews> ClassifiedNews = new();


    }
}
