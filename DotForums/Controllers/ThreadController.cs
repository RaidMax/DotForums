using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using DotForums.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace DotForums.Controllers
{
    [Route("api/[controller]")]
    public class ThreadController : BaseController<>
    {
        // GET api/Thread/ID|Slug
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string ID, [FromQuery] string include)
        {
            ulong threadID;
            UInt64.TryParse(ID, out threadID);
            ICollection<ThreadModel> Thread = null;
            return ((Thread = await _context.Threads.GetAsync(threadID, (t => t.ID == threadID || t.Slug == ID), include ?? "Author")) != null && Thread.Count > 0) ?
                (IActionResult)Ok(Thread) : NotFound(new ForumError() { Code = ForumError.ErrorCodes.THREAD_NOTFOUND });
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]IDictionary<string, string> Body)
        {
            ThreadModel Thread = null;
            return ((Thread = await _context.Threads.CreateAsync(Body)) == null) ?
                (IActionResult)StatusCode(StatusCodes.Status400BadRequest) : Ok(Thread);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(ulong ID, [FromBody]IDictionary<string, object> Body)
        {
            ThreadModel Thread = null;
            return ((Thread = await _context.Threads.UpdateAsync(ID, Body)) == null) ?
                (IActionResult)NotFound() : Ok(Thread);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
