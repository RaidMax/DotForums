using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotForums.Models
{
    public class WarningModel : ForumObjectModel
    {
        public string Reason { get; set; }
        public UserModel Origin { get; set; }
        public DateTime Expires { get; set; }

        public bool DoesExpire()
        {
            return Expires != DateTime.MaxValue;
        }
    }
}
