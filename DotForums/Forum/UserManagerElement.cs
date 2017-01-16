using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotForums.Models;
using Microsoft.EntityFrameworkCore;

namespace DotForums.Forum
{
    public class UserManagerElement : IManagerElement<UserModel>
    {
        public async Task<UserModel> CreateAsync(object[] parameters)
        {
            throw new NotImplementedException();
        }

        public async Task<UserModel> DeleteAsync(ulong ID)
        {
            throw new NotImplementedException();
        }

        public async Task<UserModel> GetAsync(ulong ID)
        {
            return await Manager.GetContext().forumContext.Users.Include(u => u.Group)
               .SingleOrDefaultAsync(u => u.ID == ID);
        }

        public async Task<List<UserModel>> GetAllAsync()
        {
            return await Manager.GetContext().forumContext.Users.ToListAsync();
        }

        public async Task<UserModel> UpdateAsync(UserModel toUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
