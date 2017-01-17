using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotForums.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Data.SqlClient;

namespace DotForums.Forum
{
    public class UserManagerElement : IManagerElement<UserModel>
    {
        private readonly ForumContext _context;

        public UserManagerElement()
        {
            _context = new ForumContext();
        }

        public async Task<UserModel> CreateAsync(IDictionary<string, string> Body)
        {
            var User = new UserModel(Body["ip"])
            {
                Username = Body["username"],
                Email = Body["email"],
                Groups = new List<GroupModel> { await Manager.GetContext().forumContext.Groups.FindAsync((ulong)2) }
            };

            try
            {
                await _context.Users.AddAsync(User);
                await _context.SaveChangesAsync();
                return await _context.Users.FirstOrDefaultAsync(u => u.Username == User.Username);
            }

            catch (InvalidOperationException e)
            {
                return null;
            }

            catch (DbUpdateException e)
            {
                _context.Users.Remove(User);
                await _context.SaveChangesAsync();
                return null;
            }
        }

        public async Task<UserModel> DeleteAsync(ulong ID)
        {
            var User = await _context.Users
                .Include(u => u.Threads)
                .SingleOrDefaultAsync(u => u.ID == ID);

            if (User != null)
            {
                try
                {
                    _context.Threads.RemoveRange(User.Threads);
                    //_context.Users.Remove(User);
                    await _context.SaveChangesAsync();
                }

                catch (DbUpdateConcurrencyException ex)
                {          
                    return null;
                }

                return User;
            }

            return null;
        }

        public async Task<UserModel> GetAsync(ulong ID)
        {
            return await _context.Users
               .Include(u => u.Groups)
               .SingleOrDefaultAsync(u => u.ID == ID);
        }

        public async Task<List<UserModel>> GetAsync()
        {
            return await Manager.GetContext().forumContext.Users.ToListAsync();
        }

        public async Task<List<UserModel>> GetAsync(Expression<Func<UserModel, bool>> lamda, int index = 0, int size = 25)
        {
            return await Display.PaginatedList<UserModel>.CreateAsync(Manager.GetContext().forumContext.Users, index, size);
        }

        public async Task<UserModel> UpdateAsync(ulong ID, IDictionary<string, object> Body)
        {
            var Existing = await _context.Users.FindAsync(ID);
            if (Existing != null)
            {
                var Model = _context.Users.Attach(Existing);
                foreach (string Key in Body.Keys)
                {
                    var Property = Model.Property(Key);
                    if (Property != null)
                        Property.CurrentValue = Body[Key];
                }

                try
                {
                    await _context.SaveChangesAsync();
                    return Existing;
                }
                catch (InvalidOperationException e)
                {
                    return null;
                }

                catch (DbUpdateException e)
                {
                    return null;
                }
            }

            return null;                 
        }
    }
}
