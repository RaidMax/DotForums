using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using DotForums.Models;
using Microsoft.EntityFrameworkCore;
using System;

using DotForums.Forum;
using System.Threading.Tasks;

namespace DotForums.Controllers
{
    [Route("api/[controller]")]
    public abstract class BaseController<T> : Controller
    {
        protected static readonly Manager _managerinstance = Manager.GetInstance();
        [HttpGet]
        public abstract Task<IActionResult> Get(QueryOptions Options);

        [HttpGet("{ID}")]
        public abstract Task<IActionResult> Get(ulong ID);

        [HttpPost]
        public abstract Task<IActionResult> Post([FromBody]T Value);

        [HttpPut("{ID}")]
        public abstract Task<IActionResult> Put(ulong ID, [FromBody]T Value);

        [HttpDelete("{ID}")]
        public abstract Task<IActionResult> Delete(ulong ID);
    }
}
