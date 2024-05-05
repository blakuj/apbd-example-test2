using Microsoft.AspNetCore.Mvc;
using WebApplication2.Repository;

namespace WebApplication2.Controllers;
[ApiController]
[Route("api/[controller]")]
public class StudentController : ControllerBase
{
    private readonly StudentRepository _studentRepository;
    public StudentController(StudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> deleteStudent(int id)
    {
        
        if (!await _studentRepository.doesStudentExist(id))
        {
            return NotFound($"Student with given id: {id} does not exist");
        }

        _studentRepository.deleteStudent(id);
        
        return NoContent();
    }

}