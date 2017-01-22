using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using DotForums.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace DotForums.Controllers
{
    [Route("api/[controller]")]
    public class FileController : Controller
    {
        private readonly Forum.Manager _context = Forum.Manager.GetContext();
        [HttpGet("{FileName}")]
        public async Task<IActionResult> Get(string FileName)
        {
            var File = await new ForumContext().Files.Where(f => f.FileName == FileName).FirstOrDefaultAsync();
            if (File == null)
                return NotFound();
            return (File.Data == null) ? (IActionResult)StatusCode(StatusCodes.Status204NoContent) : new FileStreamResult(new MemoryStream(File.Data), File.ContentType);
        }
    }
}
