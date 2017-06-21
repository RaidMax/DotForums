using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DotForums.Services;
using DotForums.Models;
using DotForums.Domain;

namespace DotForums.Forum
{
   public class Manager
    {
        private static Manager _instance;
        public ForumContext ForumContext { get; private set; }

        public IService<CategoryModel> Categories { get; private set; }
        public IService<UserDTO> Users { get; private set; }
        public IService<ThreadDTO> Threads { get; private set; }

        public static Manager GetInstance()
        {
            return _instance ?? (_instance = new Manager());
        }

        private Manager()
        {
            ForumContext = new ForumContext();
            Users = new UserService();
            Threads = new ThreadService();
        }
    }
}
