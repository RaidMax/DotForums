using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DotForums.Models
{
    public class UserGroupModel : ForumObjectModel
    {
        [ForeignKey("User")]
        public ulong UserID { get; set; }
        [Required]
        public UserModel User { get; set; }

        [ForeignKey("Group")]
        public ulong GroupID { get; set; }
        [Required]
        public GroupModel Group { get; set; }

        public UserGroupModel()
        {
            Name = "UserGroup";
        }
    }
}
