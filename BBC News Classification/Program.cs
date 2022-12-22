using System.Formats.Asn1;
using System.Globalization;
using System;
using BBC_News_Classification.Cacheable;
using BBC_News_Classification.Detectors.Abstracts;
using BBC_News_Classification.Detectors.Concretes;
using BBC_News_Classification.Models;
using CsvHelper;

var trainingPath = "C:\\Users\\berat\\source\\repos\\BBC News Classification\\BBC News Classification\\files\\BBC News Train.csv";
var testPath = "C:\\Users\\berat\\source\\repos\\BBC News Classification\\BBC News Classification\\files\\BBC News Test.csv";

ICategoryDetector categoryDetector = new CategoryDetector(trainingPath);
categoryDetector.DetectCategories();

IWordDetector wordDetector = new WordDetector(trainingPath);
wordDetector.DetectMostUsageWordByCategory();

INewsClassification classification = new NewsClassification(testPath); 
classification.DetectNewsCategory();

var item2 = CacheableData.Words.GroupBy(t => t.Category).ToList();
var item = CacheableData.ClassifiedNews.GroupBy(t => t.Category).ToList();

Console.ReadLine();
