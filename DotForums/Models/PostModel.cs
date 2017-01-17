using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotForums.Models
{
    public class PostModel : ThreadModel
    {
        public ThreadModel Parent { get; set; }
    }
}
