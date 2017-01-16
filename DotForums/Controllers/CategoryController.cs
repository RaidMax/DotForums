using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using DotForums.Models;
using Microsoft.EntityFrameworkCore;


namespace DotForums.Controllers
{
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            using (ForumContext Context = new ForumContext())
            {
                return Ok(Context.Categories
                    .Include(c => c.Permissions)
                        .ThenInclude( p => p.Group)
                    .ToList());
            }
        }

        [HttpGet("{ID}")]
        public IActionResult Get(ulong ID)
        {
            using (ForumContext Context = new ForumContext())
            {
                var Category = Context.Categories.Find(ID);
                if (Category != null)
                    return Ok(Category);
                return NotFound(new ForumError
                {
                    Code = ForumError.ErrorCodes.CATEGORY_NOTFOUND
                });
            }
        }

        [HttpGet("{ID}/Threads")]
        public IActionResult GetThreadsByCategory(ulong ID)
        {
            using (ForumContext Context = new ForumContext())
            {
                var Threads = Context.Categories
                    .Include(c => c.Threads)
                    .Where(c => c.ID == ID);
                return Ok(Threads.ToList());
            }
        }

        [HttpGet("{ID}/Threads/Posts")]
        public IActionResult GetThreadsAndPostsByCategory(ulong ID)
        {
            using (ForumContext Context = new ForumContext())
            {
                var Threads = Context.Categories
                    .Include(c => c.Threads)
                    .Where(c => c.ID == ID);
                return Ok(Threads.ToList());
            }
        }

        [HttpPost]
        public void Post([FromBody]string value)
        {
            using (ForumContext Context = new ForumContext())
            {

            }
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
            using (ForumContext Context = new ForumContext())
            {

            }
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            using (ForumContext Context = new ForumContext())
            {

            }
        }
    }
}
