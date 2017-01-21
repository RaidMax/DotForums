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
        private ICollection<UserGroupModel> _members;
        public virtual ICollection<UserGroupModel> Members
        {
            get
            {
                return _members ?? (_members = new List<UserGroupModel>());
            }
        }

        public GroupModel()
        {
            Name = "GroupModel";
        }
    }
}
