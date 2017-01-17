using System;
using System.ComponentModel.DataAnnotations;

namespace DotForums.Models
{
    public class GroupModel : ForumObjectModel
    {
        [Required]
        public string Title { get; set; }
        public int Members { get; set; }

        public GroupModel()
        {
            Name = "GroupModel";
        }
    }
}
