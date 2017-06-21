using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotForums.Domain
{
    public class UserDTO : BaseDTO
    {
        public string Username { get; set; }
        public string Email { get; set;  }
        public string Password { get; set; }
        public DateTime Seen { get; set; }
        public DateTime Date { get;  set; }
        public ulong ProfileID { get; set; }
        public int PostCount { get; set; }
        public int ThreadCount { get; set; }
        public ICollection<ulong> Groups { get; set; }
    }
}
