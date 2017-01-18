using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotForums.Models
{
    public class UserGroupModel : ForumObjectModel
    {
        public ulong UserID { get; set; }
        public UserModel User { get; set; }

        public ulong GroupID { get; set; }
        public GroupModel Group { get; set; }
    }
}
