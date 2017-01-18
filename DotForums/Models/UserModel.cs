using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotForums.Models
{
    public class UserModel : ForumObjectModel
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Group is required")]
        public List<UserGroupModel> Groups { get; set; }
        public DateTime Seen { get; set; }
        public DateTime Date { get; set; } 
        public ulong ProfileID { get; set; }
        [ForeignKey("ProfileID")]
        public UserInformationModel Profile { get; set; }
        public List<ThreadModel> Threads { get; set; }
        public List<PostModel> Posts { get; set; }
        /*
        public ICollection<ThreadModel> subscribedThreads { get; set; }
        public ICollection<ThreadModel> privateThreads { get; set; }
        public ICollection<WarningModel> Warnings { get; set; }*/
        public Queue<EventModel> Events { get; set; }

        private string passwordHash { get; set; }
        private string passwordSalt { get; set; }
      
        public UserModel()
        {
            Name = "UserModel";
            Threads = new List<ThreadModel>();
            Posts = new List<PostModel>();
            Events = new Queue<EventModel>();
            Profile = new UserInformationModel();
            Groups = new List<UserGroupModel>();
        }

        public UserModel(string ip) : this()
        {
            Profile.IPS.Add(new UserInformationModel.IP(ip));
        }
    }
}
