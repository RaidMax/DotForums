using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Specialized;

namespace DotForums.Models
{
    public class ThreadModel : ForumObjectModel
    {
        public ThreadModel Parent { get; set; }
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
                Slug = value.Replace(' ', '-').ToLower() + '-' + ID.ToString();
            }
        }
        [Required]
        public string Content { get; set; }
        [Required]
        public UserModel Author { get; set; }
        [Required]
        public List<PermissionModel> Permissions { get; set; }
        [Required]
        public List<ThreadModel> Posts { get; set; }
        public string Slug { get; set; }
        public DateTime Date { get; set; }
        public DateTime Modified { get; set; }

        public ThreadModel()
        {
            Permissions = new List<PermissionModel>();
            Posts = new List<ThreadModel>();
        }
    }
}
