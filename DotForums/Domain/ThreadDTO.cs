using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotForums.Domain
{
    public class ThreadDTO : BaseDTO
    {
        public string Title { get; set; }
        public ulong AuthorID { get; set; }
        public ulong CategoryID { get; set; }
        public string Content { get; set; }
    }
}
