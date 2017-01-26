using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using DotForums.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

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

        // GET api/User{?optional index/size}
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]int index, [FromQuery]int size)
        {    
            if (index < 1 || size < 1)
                return Ok(await _context.Users.GetAsync());
            return Ok(await _context.Users.GetAsync(0, null, null, index, size));
        }

        // GET api/User/{ID}
        [HttpGet("{ID}")]
        public async Task<IActionResult> Get(string ID, [FromQuery] string include)
        {
            try
            {
                ulong userID = 0;
                ICollection<UserModel> Users = null;
                UInt64.TryParse(ID.ToString(), out userID);

                if (userID > 0)
                    Users = await _context.Users.GetAsync(userID, null, include);

                else
                    Users = await _context.Users.GetAsync(0, user => user.Username == ID.ToString(), include);

                if (Users.Count == 1)
                    return Ok(Users.FirstOrDefault());

                return NotFound(new ForumError() { Code = ForumError.ErrorCodes.USER_NOTFOUND });
            }

            catch (InvalidOperationException e)
            {
                return StatusCode(400);
            }
        }

        // POST api/User
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Dictionary<string, string> Body)
        {
            Body["ip"] = HttpContext.Connection.RemoteIpAddress.ToString();
            var User = await _context.Users.CreateAsync(Body);
            if (User != null)
                return Created(User.ID.ToString(), User);

            return StatusCode(StatusCodes.Status409Conflict);
        }

        [HttpPost("{ID}")]
        public async Task<IActionResult> Put(ulong ID,ICollection<IFormFile> files = null)
        {
            var Body = new Dictionary<string, object>();
            if (files != null && files.Count == 1)
            {
                Body.Add("Avatar", files.First());

                var User = await _context.Users.UpdateAsync(ID, Body);
                if (User != null)
                    return Ok(User);
                return NotFound(new ForumError() { Code = ForumError.ErrorCodes.USER_NOTFOUND });
            }

            return StatusCode(StatusCodes.Status400BadRequest);
        }

        // PUT api/User/{ID} (updating a user profile)
        [HttpPut("{ID}")]
        public async Task<IActionResult> Put(ulong ID, [FromBody]Dictionary<string, object> Body, ICollection<IFormFile> files = null)
        {
            if (files != null && files.Count == 1)
                Body.Add("Avatar", files.First());

            var User = await _context.Users.UpdateAsync(ID, Body);
            if (User != null)
                return Ok(User);
            return NotFound(new ForumError() { Code = ForumError.ErrorCodes.USER_NOTFOUND });
        }

        // DELETE api/User/{ID}
        [HttpDelete("{ID}")]
        public async Task<IActionResult> Delete(ulong ID)
        {
            var User = await _context.Users.DeleteAsync(ID);
            if (User != null)
                return Ok(User);
            return NotFound(new ForumError() { Code = ForumError.ErrorCodes.USER_NOTFOUND });
        }

        #region GET_USER_INFORMATION
        [HttpGet("{ID}/Threads")]
        public async Task<IActionResult> GetThreadsByUser(ulong ID, [FromQuery]int index, [FromQuery]int size)
        {
           if (index > 0 && size > 1)
                return Ok(await _context.Threads.GetAsync(0, (t => t.AuthorID == ID), null, index, size));

            return StatusCode(StatusCodes.Status400BadRequest);
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
