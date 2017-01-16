using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DotForums.Models
{
    public class UserModel : ForumObjectModel
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Group is required")]
        public GroupModel Group { get; set; }

        public DateTime lastLogin { get; set; }
        public DateTime Date { get; set; } 
        public AvatarModel Avatar { get; set; }
        //public int Threads { get; set; }
        public int Posts { get; set; }

        /*public List<ThreadModel> Threads { get; set; }
        public ICollection<ThreadModel> subscribedThreads { get; set; }
        public ICollection<ThreadModel> privateThreads { get; set; }
        public ICollection<WarningModel> Warnings { get; set; }*/
        public Queue<EventModel> Events { get; set; }

        private string passwordHash { get; set; }
        private string passwordSalt { get; set; }

        public UserModel()
        {
            //Threads = new List<ThreadModel>();
            Events = new Queue<EventModel>();
        }
    }
}
