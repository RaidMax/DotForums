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
        public async Task<IActionResult> Get(ulong ID, [FromQuery] string include)
        {
            try
            {
                var User = await _context.Users.GetAsync(ID, null, include);
                if (User != null)
                    return Ok(User);

                return NotFound(new ForumError() { Code = ForumError.ErrorCodes.USER_NOTFOUND });
            }

            catch (InvalidOperationException)
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

            return StatusCode(409);
        }

        // PUT api/User/{ID}
        [HttpPut("{ID}")]
        public async Task<IActionResult> Put(ulong ID, [FromBody]Dictionary<string, object> Body)
        {
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
