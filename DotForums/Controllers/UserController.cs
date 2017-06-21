using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using DotForums.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using DotForums.Domain;
using DotForums.Forum;

namespace DotForums.Controllers
{
    [Route("api/[controller]")]
    public class UserController : BaseController<UserDTO>
    {
        public override async Task<IActionResult> Delete(ulong ID)
        {
            return Ok(await _managerinstance.Users.DeleteAsync(ID)) as IActionResult;
        }

        public override IActionResult Get(QueryOptions Options)
        {
            throw new NotImplementedException();
        }

        public override IActionResult Get(ulong ID)
        {
            throw new NotImplementedException();
        }

        public override IActionResult Post([FromBody] UserDTO Value)
        {
            throw new NotImplementedException();
        }

        public override IActionResult Put(ulong ID, [FromBody] UserDTO Value)
        {
            throw new NotImplementedException();
        }
    }
}
