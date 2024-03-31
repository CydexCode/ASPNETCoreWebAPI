using ASPNETCoreWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASPNETCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly StudentDbContext _studentContext;
        public StudentsController(StudentDbContext studentContext)
        {
            _studentContext = studentContext;
        }

        // Get : api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            if (_studentContext.Students == null)
            {
                return NotFound();
            }
            return await _studentContext.Students.ToListAsync();
        }

        // Get : api/Students/2
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            if (_studentContext.Students is null)
            {
                return NotFound();
            }
            var student = await _studentContext.Students.FindAsync(id);
            if (student is null)
            {
                return NotFound();
            }
            return student;
        }

        // Post : api/Students
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(Student student)
        {
            _studentContext.Students.Add(student);
            await _studentContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetStudent), new { id = student.Id }, student);
        }

        // Put : api/Movies/2
        [HttpPut]
        public async Task<ActionResult<Student>> PutStudent(int id, Student student)
        {
            if (id != student.Id)
            {
                return BadRequest();
            }
            _studentContext.Entry(student).State = EntityState.Modified;
            try
            {
                await _studentContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id)) { return NotFound(); }
                else { throw; }
            }
            return NoContent();
        }

        private bool StudentExists(long id)
        {
            return (_studentContext.Students?.Any(student => student.Id == id)).GetValueOrDefault();
        }


               // Delete : api/Students/2
        [HttpDelete("{id}")]
        public async Task<ActionResult<Student>> DeleteStudent(int id)
        {
            if (_studentContext.Students is null)
            {
                return NotFound();
            }
            var student= await _studentContext.Students.FindAsync(id);
            if (student is null)
            {
                return NotFound();
            }
          _studentContext.Students.Remove(student);
            await _studentContext.SaveChangesAsync();
            return NoContent();
        }

    }



}
