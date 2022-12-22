using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBC_News_Classification.Models
{
    /// <summary>
    /// Keep Category Usage
    /// </summary>
    public class CategoryUsage
    {
        public int Count { get; set; }

        public string? Category { get; set; }

        public int  Index { get; set; }
    }
}
