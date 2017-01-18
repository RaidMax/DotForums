using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotForums.Models
{
    public class PostModel : ForumObjectModel
    {
        public ThreadModel Parent { get; set; }
        public DateTime Date { get; set; }
        public DateTime Modified { get; set; }
        public UserModel Author { get; set; }
        public string Content { get; set; }
    }
}
