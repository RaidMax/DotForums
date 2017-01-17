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
        public List<GroupModel> Groups { get; set; }
        public DateTime Seen { get; set; }
        public DateTime Date { get; set; } 
       // public UserInformationModel Profile { get; set; }
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
            //Profile = new UserInformationModel();
            Groups = new List<GroupModel>();
        }

        public UserModel(string ip) : base()
        {
            // https://blogs.msdn.microsoft.com/ericlippert/2008/02/15/why-do-initializers-run-in-the-opposite-order-as-constructors-part-one/
           // Profile = new UserInformationModel(this);
           // Profile.IPS.Add(new UserInformationModel.IP(ip));
        }
    }
}
