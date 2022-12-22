using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBC_News_Classification.Aho_Corasick
{
    public interface IAhoCorasick
    {
        void Add(IEnumerable<string> value);
    }
}
