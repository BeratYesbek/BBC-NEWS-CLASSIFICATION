using System.Formats.Asn1;
using System.Globalization;
using System;
using BBC_News_Classification.Cacheable;
using BBC_News_Classification.Detectors.Abstracts;
using BBC_News_Classification.Detectors.Concretes;
using BBC_News_Classification.Models;
using CsvHelper;

ICategoryDetector categoryDetector = new CategoryDetector("C:\\Users\\berat\\Desktop\\BBC News Train.csv");
categoryDetector.DetectCategories();

IWordDetector wordDetector = new WordDetector("C:\\Users\\berat\\Desktop\\BBC News Train.csv");
wordDetector.DetectMostUsageWordByCategory();

INewsClassification classification = new NewsClassification("C:\\Users\\berat\\Desktop\\BBC News Test.csv");
classification.DetectNewsCategory();

var item2 = CacheableData.Words.GroupBy(t => t.Category).ToList();
var item = CacheableData.ClassifiedNews.GroupBy(t => t.Category).ToList();

Console.ReadLine();
