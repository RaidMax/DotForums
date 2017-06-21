using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotForums.Forum
{
    public class QueryOptions
    {
        public IList<string> Includes { get; set; }
        public int Limit { get; set; }
        public string SortBy { get; set; }
        public int SortDirection { get; set; }
        public string FilterBy { get; set; }
        public string FilterValue { get; set; }
    }
}
