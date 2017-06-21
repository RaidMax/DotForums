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
using DotForums.Forum.Display;
using DotForums.Domain;

namespace DotForums.Services
{
    public class UserService : IService<UserDTO>
    {
        private readonly ForumContext _context = new ForumContext();

        public async Task<UserDTO> CreateAsync(UserDTO Value)
        {
            var User = new UserModel()
            {
                Username = Value.Username,
                Email = Value.Email
            };

            // fixme: throw exception?
            if (await _context.Users.Where(u => u.Username == User.Username || u.Email == User.Email).FirstOrDefaultAsync() != null)
                return null;

            await User.SetPasswordAsync(Value.Password);

            User.Groups.Add(new UserGroupModel
            {
                GroupID = 2,
                User = User
            });

            try
            {
                // checkme: why does AddAsync cause PK = 0?
                _context.Users.Add(User);
                await _context.SaveChangesAsync();
                Value.ID = User.ID;
                return Value;
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

        public async Task<UserDTO> DeleteAsync(ulong ID)
        {
            var User = await _context.Users
                .SingleOrDefaultAsync(u => u.ID == ID);

            if (User != null)
            {
                try
                {
                    _context.Users.Remove(User);
                    await _context.SaveChangesAsync();
                }

                catch (DbUpdateConcurrencyException ex)
                {
                    // logme
                    return null;
                }

                return new UserDTO()
                {
                    ID = User.ID,
                    ProfileID = User.ProfileID,
                    Email = User.Email
                };
            }

            return null;
        }

        public async Task<ICollection<UserDTO>> GetAsync(ulong ID, QueryOptions Options)
        {
            var DTOList = new List<UserDTO>();
            if (ID > 0)
            {
                var User = await _context.Users.SingleOrDefaultAsync(u => u.ID == ID);
                DTOList.Add(new UserDTO()
                {
                    ID = User.ID,
                    Username = User.Username,
                    Email = User.Email,
                    Seen = User.Seen,
                    Date = User.Date,
                    ProfileID = User.ProfileID,
                    PostCount = User.Posts.Count(),
                    ThreadCount = User.Threads.Count()
                });
            }

            else
            {
                // todo: apply queryoptions
                var Users = await _context.Users.ToListAsync();
                foreach (var User in Users)
                {
                    DTOList.Add(new UserDTO()
                    {
                        ID = User.ID,
                        Username = User.Username,
                        Email = User.Email,
                        Seen = User.Seen,
                        Date = User.Date,
                        ProfileID = User.ProfileID,
                        PostCount = User.Posts.Count(),
                        ThreadCount = User.Threads.Count()
                    });
                }
            }

            return DTOList;
          /*  if (index > 0 && size > 0 && ID == 0)
            {
                return await PaginatedList<UserModel>.CreateAsync(_context.Users, index, size);
            }

            else if (ID > 0)
            {
                if (lamda != null)
                {
                    return await _context.Users
                        .Include(include ?? "Profile")
                        .Where(u => u.ID == ID)
                        .Where(lamda)
                        .ToListAsync();
                }

                return await _context.Users
                        .Include(include ?? "Profile")
                        .Where(u => u.ID == ID)
                        .ToListAsync();
            }

            else if (lamda != null)
                return await _context.Users
                    .Include(include)
                    .Where(lamda)
                    .ToListAsync();

            else
                return await _context.Users.ToListAsync();*/
        }

        public async Task<UserDTO> UpdateAsync(ulong ID, UserDTO Value)
        {
            var User = await _context.Users.FindAsync(ID);
            
            if (User != null)
            {
                var TrackedUser = _context.Users.Attach(User);
                User.Email = Value.Email ?? User.Email;
                User.Username = Value.Username ?? User.Username;
                await _context.SaveChangesAsync();
            }

            return null;

            /*
            if (Existing != null)
            {
                var Model = _context.Users.Attach(Existing);
                //FileModel Avatar = null;
                if (Body.ContainsKey("Avatar"))
                {
                    var File = (IFormFile)Body["Avatar"];
                    if (FileModel.GetFileType(File.FileName) == FileModel.FileType.IMAGE)
                    {
                        byte[] AvatarArray = null;
                        using (var Stream = new MemoryStream())
                        {
                            await File.CopyToAsync(Stream);
                            // checkme: restrict max file size
                            AvatarArray = Stream.ToArray();
                        }


                        var Avatar = new FileModel()
                        {
                            Title = String.Format("{0}'s Avatar", Existing.Username),
                            Data = AvatarArray,
                            FileName = File.FileName,
                            Type = FileModel.FileType.IMAGE,
                            ContentType = File.ContentType
                        };

                        _context.Files.Add(Avatar);
                        Existing.Profile.Avatar = Avatar;
                        await _context.SaveChangesAsync();
                        
                    }

                    Body.Remove("Avatar");
                }

                try
                {
                    foreach (string Key in Body.Keys)
                    {
                        var Property = Model.Property(Key);
                        if (Property != null)
                            Property.CurrentValue = Body[Key];
                    }

                    // we only want the update to happen if all keys are valid. 
                    await _context.SaveChangesAsync();
                    return Existing;
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

            return null;*/
        }
    }
}
