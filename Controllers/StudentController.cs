using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SVAPI.Models;
namespace SVAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController: ControllerBase
    {
        private readonly StudentContext _context;
        public StudentController(StudentContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> Get()
        {
            return await _context.Students.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(long id)
        {
            var todoItem = await _context.Students.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }
            return todoItem;
        }
        [HttpPost]
        public async Task<ActionResult<Student>> Post(Student item)
        {
            _context.Students.Add(item);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetStudent), new { id = item.id }, item);
        }
    }
}