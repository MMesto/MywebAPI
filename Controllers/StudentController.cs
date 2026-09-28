using Microsoft.AspNetCore.Mvc;
using MywebAPI.Models;              // zodat je Student kunt gebruiken

namespace MywebAPI.Controllers;

[Route("api/[controller]")]         // adres wordt: /api/Student
[ApiController]
public class StudentController : ControllerBase
{
    // GET: api/Student
    [HttpGet]
    public string GetString()
    {
        return "Het endpoint werkt!";
    }

    // GET: api/Student/student
    [HttpGet("student")]
    public Student GetStudent()
    {
        Student student = new Student(1, "25012345", "Jelle");
        return student;
    }

    // GET: api/Student/studentmetstatus
    [HttpGet("studentmetstatus")]
    public ActionResult<Student> GetStudentMetStatus()
    {
        Student student = new Student(1, "25012345", "Jelle");
        if (student.Naam == "Jelle")
        {
            return NotFound();      // stuurt statuscode 404 terug
        }
        return student;
    }

    // GET: api/Student/3
    [HttpGet("{id}")]
    public ActionResult<Student> GetStudentById(int id)
    {
        Student? student = Student.Students.FirstOrDefault(s => s.Id == id);
        if (student == null)
        {
            return NotFound();      // id bestaat niet → 404
        }
        return student;             // gevonden → 200 + JSON
    }

    // GET: api/Student/async/3
    [HttpGet("async/{id}")]
    public async Task<ActionResult<Student>> GetStudentByIdAsync(int id)
    {
        Student? student = await Task.FromResult(
            Student.Students.FirstOrDefault(s => s.Id == id));
        if (student == null)
        {
            return NotFound();
        }
        return student;
    }
}