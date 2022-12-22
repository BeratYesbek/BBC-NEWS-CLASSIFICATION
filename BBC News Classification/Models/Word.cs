using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBC_News_Classification.Models
{
    public class Word
    {
        public string? Content { get; set; }
        public string? Category { get; set; }
        public int Count { get; set; }
    }
}