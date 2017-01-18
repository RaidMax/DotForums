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
        [Required]
        public UserModel Author { get; set; }
        [Required]
        public List<PermissionModel> Permissions { get; set; }
        [Required]
        public List<PostModel> Posts { get; set; }
        public string Slug { get; set; }
        public DateTime Date { get; set; }
        public DateTime Modified { get; set; }

        public ThreadModel()
        {
            Name = "Thread";
            Permissions = new List<PermissionModel>();
            Posts = new List<PostModel>();
        }
    }
}
