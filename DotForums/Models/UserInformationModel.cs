using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace DotForums.Models
{
    public class UserInformationModel : ForumObjectModel
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

        public List<IP> IPS { get; set; }
        [ForeignKey("Avatar")]
        public ulong AvatarID { get; set; }
        public ImageModel Avatar { get; set; }

        public UserInformationModel()
        {
            IPS = new List<IP>();
            Avatar = new ImageModel();
        }
    }
}
