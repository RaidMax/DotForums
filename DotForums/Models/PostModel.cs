﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DotForums.Models
{
    public class PostModel : ForumObjectModel
    {
        [ForeignKey("Parent")]
        public ulong ParentID { get; set; }
        [Required]
        public ThreadModel Parent { get; set; }
        public DateTime Date { get; set; }
        public DateTime Modified { get; set; }
        [ForeignKey("Author")]
        public ulong AuthorID { get; set; }
        [Required]
        public UserModel Author { get; set; }
        [MaxLength(2000)]
        public string Content { get; set; }
        private ICollection<FileModel> _attachments;
        public virtual ICollection<FileModel> Attachments
        {
            get
            {
                return _attachments ?? (_attachments = new List<FileModel>());
            }
        }

        public PostModel()
        {
            Name = "PostModel";
        }
    }
}
