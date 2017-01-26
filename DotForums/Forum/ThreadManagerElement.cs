using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotForums.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace DotForums.Forum
{
    public class ThreadManagerElement : IManagerElement<ThreadModel>
    {
        private readonly ForumContext _context;

        public ThreadManagerElement()
        {
            _context = new ForumContext();
        }

        public async Task<ThreadModel> CreateAsync(IDictionary<string, string> Body)
        {
            ThreadModel Thread = null;
            try
            {
                Thread = new ThreadModel()
                {
                    Title = Body["Title"],
                    AuthorID = Convert.ToUInt64(Body["Author"]),
                    CategoryID = Convert.ToUInt64(Body["Category"]),
                };

                PostModel Post = new PostModel()
                {
                    AuthorID = Thread.AuthorID,
                    Content = Body["Content"],
                    Parent = Thread,
                };

                Thread.Posts.Add(Post);

                _context.Threads.Add(Thread);
                await _context.SaveChangesAsync();
            }

            catch (KeyNotFoundException)
            {
                // log me
            }

            return Thread;
        }

        public async Task<ThreadModel> DeleteAsync(ulong ID)
        {
            var Thread = await _context.Threads.FindAsync(ID);
            if (Thread != null)
            {
                _context.Threads.Remove(Thread);
                await _context.SaveChangesAsync();
            }

            return Thread;
        }

        public async Task<ICollection<ThreadModel>> GetAsync(ulong ID = 0, Expression<Func<ThreadModel, bool>> lamda = null, string include = "Author", int index = 0, int size = 0)
        {
            if (ID > 0)
            {
                return new List<ThreadModel>
                {
                    await _context.Threads
                        .Include(include ?? "Author")
                        .Include(t => t.Category)
                        .Include(t => t.Author)
                        .SingleOrDefaultAsync(t => t.ID == ID)
                };
            }

            return await _context.Threads
                    .Include(include ?? "Author")
                    .Include(t => t.Category)
                    .Include(t => t.Author)
                    .Where(lamda).ToListAsync();
        }

        public async Task<ThreadModel> UpdateAsync(ulong ID, IDictionary<string, object> Body)
        {
            var Thread = await _context.Threads.FindAsync(ID);

            if (Thread != null)
            {
                try
                {
                    var Model = _context.Threads.Attach(Thread);
                    foreach (string Key in Body.Keys)
                    {
                        var Property = Model.Property(Key);
                        if (Property != null)
                            Property.CurrentValue = Body[Key];
                   
                    }
                    // we only want the update to happen if all keys are valid. 
                    Model.Property(t => t.Modified).CurrentValue = DateTime.Now;
                    await _context.SaveChangesAsync();
                }

                catch (InvalidOperationException e)
                {
                    // logme
                    return null;
                }

                catch (DbUpdateException e)
                {
                    // logme
                    return null;
                }

                catch (FormatException e)
                {
                    // logme
                    return null;
                }

                catch (InvalidCastException e)
                {
                    // logme
                    return null;
                }

                catch (NullReferenceException e)
                {
                    // logme
                    return null;
                }
            }

            return Thread;
        }
    }
}
