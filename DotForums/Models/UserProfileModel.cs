using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace DotForums.Models
{
    public class UserProfileModel : ForumObjectModel
    {
        public class IP : ForumObjectModel
        {
            public string Address { get; set; }
            public DateTime Time { get; set; }
            public IP(string ip)
            {
                Time = DateTime.Now;
                Address = ip;
            }
        }

        private ICollection<IP> _ips;
        public virtual ICollection<IP> IPS
        {
            get
            {
                return _ips ?? (_ips = new List<IP>());
            }
        }

        [ForeignKey("Avatar")]
        public ulong AvatarID { get; set; }
        [Required]
        public FileModel Avatar { get; set; }
        private ICollection<AttributeModel> _attributes;
        public virtual ICollection<AttributeModel> Attributes
        {
            get
            {
                return _attributes ?? (_attributes = new List<AttributeModel>());
            }
        }

        public UserProfileModel()
        {
            Avatar = new FileModel()
            {
                Title = "Default Avatar",
                FileName = "DefaultAvatar.png",
            };

            Name = "UserProfile";
        }
    }
}
