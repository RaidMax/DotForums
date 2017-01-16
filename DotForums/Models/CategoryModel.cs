using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


namespace DotForums.Models
{
    public class CategoryModel : ForumObjectModel
    {
        [Required]
        public string Description { get; set; }
        [Required]
        public List<PermissionModel> Permissions { get; set; }
        [Required]
        public List<CategoryModel> Children { get; set; }
        [Required]
        public List<ThreadModel> Threads { get; set; }
        public ulong Count { get; set; }

        public CategoryModel()
        {
            Permissions = new List<PermissionModel>();
            Threads = new List<ThreadModel>();
            Children = new List<CategoryModel>();
        }
    }
}
