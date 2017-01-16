using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using DotForums.Models;
using Microsoft.EntityFrameworkCore;

namespace DotForums.Controllers
{
    [Route("api/[controller]")]
    public class BasController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            using (ForumContext Context = new ForumContext())
            {
                return null;
            }
        }

        [HttpGet("{ID}")]
        public IActionResult Get(ulong ID)
        {
            using (ForumContext Context = new ForumContext())
            {
                return null;
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
