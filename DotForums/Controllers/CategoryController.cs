using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using DotForums.Models;
using Microsoft.EntityFrameworkCore;

using DotForums.Forum;

namespace DotForums.Controllers
{
    [Route("api/[controller]")]
    public class CategoryController : BaseController<CategoryModel>
    {
  

        /*
        [HttpGet("{ID}/Threads")]
        public IActionResult GetThreadsByCategory(ulong ID)
        {
            var Context = Manager.GetInstance().ForumContext;
            var Threads = Context.Categories
                    .Include(c => c.Threads)
                    .Where(c => c.ID == ID);
                return Ok(Threads.ToList());
        }

        [HttpGet("{ID}/Threads/Posts")]
        public IActionResult GetThreadsAndPostsByCategory(ulong ID)
        {
                var Context = Manager.GetInstance().ForumContext;
                var Threads = Context.Categories
                    .Include(c => c.Threads)
                    .Where(c => c.ID == ID);
                return Ok(Threads.ToList());
        }*/
        public override IActionResult Get(QueryOptions q)
        {
            throw new NotImplementedException();
        }

        public override IActionResult Get(ulong ID)
        {
            var Context = Manager.GetInstance().ForumContext;
            var Category = Context.Categories.Find(ID);
            if (Category != null)
                return Ok(Category);
            return NotFound(new ForumError
            {
                Code = ForumError.ErrorCodes.CATEGORY_NOTFOUND
            });
        }

        public override IActionResult Post(CategoryModel Value)
        {
            throw new NotImplementedException();
        }

        public override IActionResult Put(ulong ID, CategoryModel Value)
        {
            return Ok(Manager.GetInstance().Categories.CreateAsync(Value));
        }

        public override IActionResult Delete(ulong ID)
        {
            return Ok(Manager.GetInstance().Categories.DeleteAsync(ID));
        }
    }
}
