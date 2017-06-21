using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotForums.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;
using System.IO;

using DotForums.Forum;
using DotForums.Domain;

namespace DotForums.Services
{
    public class ThreadService : IService<ThreadDTO>
    {
        private readonly ForumContext _context = new ForumContext();

        public async Task<ThreadDTO> CreateAsync(ThreadDTO Value)
        {
            ThreadModel Thread = null;
            Thread = new ThreadModel()
            {
                Title = Value.Title,
                AuthorID = Value.AuthorID,
                CategoryID = Value.CategoryID
            };

            PostModel Post = new PostModel()
            {
                AuthorID = Thread.AuthorID,
                Content = Value.Content,
                Parent = Thread,
            };

            Thread.Posts.Add(Post);
            _context.Threads.Add(Thread);

            await _context.SaveChangesAsync();
            Value.ID = Thread.ID;


            return Value;
        }

        public async Task<ThreadDTO> DeleteAsync(ulong ID)
        {
            var Thread = await _context.Threads.FindAsync(ID);
            if (Thread != null)
            {
                _context.Threads.Remove(Thread);
                await _context.SaveChangesAsync();
            }

            return new ThreadDTO()
            {
                ID = Thread.ID,
                AuthorID = Thread.AuthorID,
                CategoryID = Thread.CategoryID,
                Title = Thread.Title
            };
        }

        public async Task<ICollection<ThreadDTO>> GetAsync(ulong ID, QueryOptions Options)
        {
            var DTOList = new List<ThreadDTO>();
            if (ID > 0)
            {
                var Thread = await _context.Threads.SingleOrDefaultAsync(t => t.ID == ID);
                DTOList.Add(new ThreadDTO()
                {
                    ID = Thread.ID,
                    CategoryID = Thread.CategoryID,
                    AuthorID = Thread.AuthorID,
                    Title = Thread.Title
                });
            }
            // todo: apply queryoptions!
           else
            {
                var Threads = await _context.Threads.ToListAsync();

                foreach (var Thread in Threads)
                {
                    DTOList.Add(new ThreadDTO()
                    {
                        ID = Thread.ID,
                        CategoryID = Thread.CategoryID,
                        AuthorID = Thread.AuthorID,
                        Title = Thread.Title
                    });
                }
            }
            return DTOList;
        }

        public async Task<ThreadDTO> UpdateAsync(ulong ID, ThreadDTO Value)
        {
            var Thread = await _context.Threads.FindAsync(ID);
            if (Thread != null)
            {
               
                try
                {
                    var Model = _context.Threads.Attach(Thread);
                    Thread.CategoryID = Value.CategoryID;
                    Thread.Title = Value.Title;
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

            return Value;
        }
    }
}
