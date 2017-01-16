using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using DotForums.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace DotForums.Controllers
{
    [Route("api/[controller]")]
    public class ThreadController : Controller
    {
        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(ulong id)
        {
            using (ForumContext Context = new ForumContext())
            {
                var foundThread = Context.Threads.Find(id);
                if (foundThread != null)
                    return Ok(foundThread);
                return NotFound(new ForumError() { Code = ForumError.ErrorCodes.THREAD_NOTFOUND });
            }
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
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
    }
}
