using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotForums.Models
{
    public class ThreadModel : ForumObjectModel
    {
        [NotMapped]
        private string _title;
        [Required]
        public string Title
        {
            get
            {
                return _title;
            }

            set
            {
                _title = value;
                Slug = value?.Replace(' ', '-').ToLower() + '-' + DateTime.Now.Millisecond.ToString();
            }
        }
        [ForeignKey("Author")]
        public ulong AuthorID { get; set; }
        [Required]
        public UserModel Author { get; set; }
        [ForeignKey("Category")]
        public ulong CategoryID { get; set; }
        [Required]
        public CategoryModel Category { get; set; }
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
        private ICollection<PostModel> _posts;
        public virtual ICollection<PostModel> Posts
        {
            get
            {
                return _posts ?? (_posts = new List<PostModel>());
            }
        }
        [Required]
        public string Slug { get; set; }
        public DateTime Date { get; set; }
        public DateTime Modified { get; set; }

        public ThreadModel()
        {
            Name = "ThreadModel";
        }
    }
}
