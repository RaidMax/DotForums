using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotForums.Models
{
    public class ImageModel : ForumObjectModel
    {
        public string URL { get; set; }
        public byte[] Data { get; set; }
    }
}
