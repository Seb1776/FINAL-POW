using System;
using Microsoft.AspNetCore.Mvc;
using POWAPI.Services;
using POWAPI.Models;

namespace POWAPI.Controllers;

[Controller]
[Route("api/[controller]/[action]")]
public class StudentController : Controller
{
    private readonly IStudentService _studentService;
    //private readonly MongoDBService _mongoDBService;

    public StudentController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var allStudents = _studentService.GetAllStudents().ToList();
        return View(allStudents);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Student student)
    {
        await _studentService.AddStudent(student);
        await _mongoDBService.CreateStudentAsync(student);
        return CreatedAtAction(nameof(Get), new { id = student.Id }, student);
    }

    [HttpGet("{id}")]
    public IActionResult GetID(string id)
    {
        var student = _studentService.GetStudentById(id);
        return View(student);
    }

    [HttpPut("{id}")]
    public IActionResult Update(string id, [FromBody] Student student)
    {
        _studentService.UpdateStudent(id);
        //await _mongoDBService.UpdateStudentAsync(id, student);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(string id)
    {
        _studentService.DeleteStudent(id);
        //await _mongoDBService.DeleteStudentAsync(id);
        return NoContent();
    }
}