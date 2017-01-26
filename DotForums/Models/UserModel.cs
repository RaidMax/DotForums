using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace DotForums.Models
{
    public class UserModel : ForumObjectModel
    {
        [Required(ErrorMessage = "Username is required")]
        [MaxLength(20, ErrorMessage ="Username must be less than 21 characters")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [MaxLength(64, ErrorMessage = "Email must be less than 65 characters")]
        public string Email { get; set; }
        [Required]
        private ICollection<UserGroupModel> _groups;
        public virtual ICollection<UserGroupModel> Groups
        {
            get
            {
                return _groups ?? (_groups = new List<UserGroupModel>());
            }
        }
        public DateTime Seen { get; private set; }
        public DateTime Date { get; private set; }
        [ForeignKey("Profile")]
        public ulong ProfileID { get; set; }
        [Required]
        public UserProfileModel Profile { get; set; }
        private ICollection<ThreadModel> _threads;
        public virtual ICollection<ThreadModel> Threads
        {
            get
            {
                return _threads ?? (_threads = new List<ThreadModel>());
            }
        }
        private ICollection<PostModel> _posts;
        public virtual ICollection<PostModel> Posts
        {
            get
            {
                return _posts ?? (_posts = new List<PostModel>());
            }
        }
        private Queue<NotificationModel> _notifications;
        public Queue<NotificationModel> Notifications
        {
            get
            {
                return _notifications ?? (_notifications = new Queue<NotificationModel>());
            }
        }
        [Required(ErrorMessage ="Password is required")]
        [MaxLength(100, ErrorMessage ="Password must be less than 101 characters")]
        private string Password { get; set; }
      
        public UserModel()
        {
            Name = "UserModel";
            Profile = new UserProfileModel();
        }

        public void SetPassword(string password)
        {
            Password = Forum.Security.GeneratePassword(password, 15000);
        }

        public async Task SetPasswordAsync(string password)
        {
            await Task.Run(() =>
            {
                Password = Forum.Security.GeneratePassword(password, 15000);
            });
        }

        public async Task<bool> ValidatePasswordAsync(string input)
        {
            return await Task.Run(() =>
            {
                return Forum.Security.ValidatePassword(input, Password);
            });
        }
    }
}
