﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBC_News_Classification.Models
{
    public class TrainingNews
    {
        public int ArticleId { get; set; }
        public string? Text { get; set; }
        public string? Category { get; set; }

    }
}
