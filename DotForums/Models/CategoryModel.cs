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
        public ICollection<PermissionModel> Permissions { get; set; }
        [Required]
        public ICollection<CategoryModel> Children { get; set; }
        [Required]
        public ICollection<ThreadModel> Threads { get; set; }
        public ulong Count { get; set; }

        public CategoryModel()
        {
            Name = "CategoryModel";
        }
    }
}
