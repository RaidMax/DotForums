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
        public ICollection<PermissionModel> Permissions { get; set; }
        [Required]
        public ICollection<PostModel> Posts { get; set; }
        public string Slug { get; set; }
        public DateTime Date { get; set; }
        public DateTime Modified { get; set; }

        public ThreadModel()
        {
            Name = "ThreadModel";
        }
    }
}
