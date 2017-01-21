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
        public string Title { get; set; }
        [Required]
        private ICollection<PermissionModel> _permissions;
        public virtual ICollection<PermissionModel> Permissions
        {
            get
            {
                return _permissions ?? (_permissions = new List<PermissionModel>());
            }
        }
        [Required]
        private ICollection<CategoryModel> _children;
        public virtual ICollection<CategoryModel> Children
        {
            get
            {
                return _children ?? (_children = new List<CategoryModel>());
            }
        }
        [Required]
        private ICollection<ThreadModel> _threads;
        public virtual ICollection<ThreadModel> Threads
        {
            get
            {
                return _threads ?? (_threads = new List<ThreadModel>());
            }
        }

        public ulong Count { get; set; }

        public CategoryModel()
        {
            Name = "CategoryModel";
        }
    }
}
