using System;

namespace DotForums.Models
{
    public class AuthorThreadModel : ForumObjectModel
    {
        public ulong authorID { get; set; }
        public UserModel Author { get; set; }

        public ulong threadID { get; set; }
        public ThreadModel Thread { get; set; }
    }
}
