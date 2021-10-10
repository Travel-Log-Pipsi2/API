using Microsoft.AspNetCore.Mvc;
using Storage.DataAccessLayer;
using Storage.Models;
using System.Collections.Generic;
using System.Linq;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public PostController(ApiDbContext context)
        {
            _context = context;
        }

        [HttpPost()]
        public ActionResult<Test> Create(string value)
        {
            _context.TestModels.Add(new Test { TestValue = value });
            _context.SaveChanges();

            return Ok("Resource created");
        }

        [HttpGet]
        public IEnumerable<Test> Index()
        {
            return _context.TestModels.ToArray();
        }
    }

}
