using Microsoft.AspNetCore.Mvc;
using WebApiMongoDb.Server.Models;
using WebApiMongoDb.Server.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiMongoDb.Server.Controllers
{
    [Route("api/student")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly StudentServices _studentServices;

        public StudentController(StudentServices studentServices)
        {
            _studentServices = studentServices;
        }
        // GET: api/student
        [HttpGet]
        public async Task<List<Student>> Get()
        {
            return  await _studentServices.GetAsync();
        }

        // GET api/student/65c5383e2d912f5314e598ae
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Student>> Get(string id)
        {
            Student student = await _studentServices.GetAsync(id);
            if(student == null)
            {
                return NotFound();
            }
            return student;
        }

        // POST api/student
        [HttpPost]
        public async Task<ActionResult<Student>> Post(Student newStudent)
        {
            await _studentServices.CreateAsync(newStudent);
            return CreatedAtAction(nameof(Get), new { id = newStudent.Id }, newStudent);
        }

        // PUT api/student/65c5383e2d912f5314e598ae
        [HttpPut("{id:length(24)}")]
        public async Task<ActionResult> Put(string id, Student updateStudent)
        {
            Student student = await _studentServices.GetAsync(id);
            if(student == null)
            {
                return NotFound("There is no student with this id: " + id);
            }
            updateStudent.Id = student.Id;
            await _studentServices.UpdateAsync(id, updateStudent);
            return Ok("Updated successfully");
        }

        // DELETE api/student/65c5383e2d912f5314e598ae
        [HttpDelete("{id:length(24)}")]
        public async Task<ActionResult> Delete(string id)
        {
            Student student = await _studentServices.GetAsync(id);
            if (student == null)
            {
                return NotFound("There is no student with this id: " + id);
            }
            
            await _studentServices.RemoveAsync(id);
            return Ok("Deleted successfully");
        }
    }
}
