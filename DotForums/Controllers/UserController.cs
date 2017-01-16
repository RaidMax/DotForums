using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using DotForums.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DotForums.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly Forum.Manager _context;
        public UserController()
        {
            _context = Forum.Manager.GetContext();
        }

        // GET: api/Users
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            // return Ok(await _context.Users.ToListAsync());
            var Test = await _context.Users.GetAllAsync();
            return Ok(Test);
        }

        // GET api/values/5
        [HttpGet("{ID}")]
        public async Task<IActionResult> Get(ulong ID)
        {
            var User = await _context.Users.GetAsync(ID);
            if (User != null)
                return Ok(User);
            return NotFound(new ForumError() { Code = ForumError.ErrorCodes.USER_NOTFOUND });
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Dictionary<string, string> User)
        {
            /*var newUser = new UserModel { Name = User["username"], Email = User["email"], Username = User["username"]};
            await _context.Users.AddAsync(newUser);
            if (await _context.SaveChangesAsync() > 0)
                return Ok(await _context.Users.FirstOrDefaultAsync(u => u.Username == User["username"]));
            return Ok(new ForumError()
            {
                Code = ForumError.ErrorCodes.USER_NOTFOUND
            });*/
             throw new NotImplementedException();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        #region GET_USER_INFORMATION
        [HttpGet("{ID}/Threads")]
        public async Task<IActionResult> GetThreadsByUser(ulong ID)
        {
            /* var Threads = await _context.Threads
                 .Where(t => t.Author.ID == ID && t.Parent == null)
                 .ToListAsync();

            return Ok(Threads); */
            throw new NotImplementedException();
        }

        [HttpGet("{ID}/Posts")]
        public async Task<IActionResult> GetPostsByUser(ulong ID)
        {
            /*var Posts = await _context.Threads
               .Where(t => t.Author.ID == ID && t.Parent != null)
               .ToListAsync();

            return Ok(Posts);*/
            throw new NotImplementedException();
        }
        #endregion
    }
}
