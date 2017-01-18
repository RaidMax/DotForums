using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DotForums.Models
{
    public class GroupModel : ForumObjectModel
    {
        [Required]
        public string Title { get; set; }
        public int Count { get; set; }
        public List<UserGroupModel> Members { get; set; }

        public GroupModel()
        {
            Members = new List<UserGroupModel>();
            Name = "GroupModel";
        }
    }
}
