using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotForums.Forum
{
    public class Manager
    {
        private static Manager _context;
        public Models.ForumContext forumContext { get; private set; }

        public UserManagerElement Users { get; private set; }
        public ThreadManagerElement Threads { get; private set; }

        public static Manager GetContext()
        {
            if (_context == null)
                _context = new Manager();
            return _context;
        }

        private Manager()
        {
            forumContext = new Models.ForumContext();
            Users = new UserManagerElement();
            Threads = new ThreadManagerElement();
        }
    }
}
