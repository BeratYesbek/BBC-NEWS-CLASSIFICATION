﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBC_News_Classification.Detectors.Abstracts
{
    public interface IWordDetector
    {
        public void DetectMostUsageWordByCategory();
    }
}