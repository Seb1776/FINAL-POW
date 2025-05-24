using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using POWAPI.Models;
using POWAPI.Services;
using POWWEB.ViewModels;

namespace POWWEB.Controllers;

public class StudentController : Controller
{
    private readonly IStudentService _studentService;

    public StudentController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    public IActionResult Index()
    {
        var viewModel = new StudentViewModel
        {
            Students = _studentService.GetAllStudents()
        };
        
        return View(viewModel);
    }

    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Add(StudentAddViewModel studentAddViewModel)
    {
        if (ModelState.IsValid)
        {
            var newRestaurant = new Student
            {
                username = studentAddViewModel.Student.username,
                password = studentAddViewModel.Student.password,
                studentName = studentAddViewModel.Student.studentName,
                studentLastName = studentAddViewModel.Student.studentLastName
            };
            
            _studentService.AddStudent(newRestaurant);
            return RedirectToAction("Index");
        }
        
        return View(studentAddViewModel);
    }

    public IActionResult Edit(ObjectId id)
    {
        if (id == ObjectId.Empty)
            return NotFound();
        
        var selectedStudent = _studentService.GetStudentById(id);
        var viewModel = new StudentAddViewModel
        {
            Student = selectedStudent
        };
        
        return View(viewModel);
    }

    [HttpPost]
    public IActionResult Edit(Student student)
    {
        try
        {
            if (ModelState.IsValid)
            {
                _studentService.UpdateStudent(student);
                return RedirectToAction("Index");
            }

            else
                return BadRequest();
        }

        catch (Exception ex)
        {
            ModelState.AddModelError("", $"Update failed! {ex.Message}");
        }
        
        var viewModel = new StudentAddViewModel
        {
            Student = student
        };

        return View(viewModel);
    }

    public IActionResult Delete(ObjectId id)
    {
        if (id == ObjectId.Empty)
            return NotFound();
        
        var selectedStudent = _studentService.GetStudentById(id);
        return View(selectedStudent);
    }

    [HttpPost]
    public IActionResult Delete(Student student)
    {
        if (student.Id == ObjectId.Empty)
        {
            ViewData["ErrorMessage"] = "Delete failed!";
            return View();
        }

        try
        {
            _studentService.DeleteStudent(student);
            TempData["StudentDeleted"] = "Student deleted!";

            return RedirectToAction("Index");
        }

        catch (Exception ex)
        {
            ViewData["ErrorMessage"] = $"Delete failed! {ex.Message}";
        }

        var selectedStudent = _studentService.GetStudentById(student.Id);
        return View(selectedStudent);
    }
    
    /*private Uri baseAddress = new Uri("https://localhost:7233/api");
    private readonly HttpClient _client;

    public StudentController()
    {
        _client = new HttpClient();
        _client.BaseAddress = baseAddress;
    }
    
    // GET
    public IActionResult Index()
    {
        var studentView = new StudentViewModel();
        studentView.Students = new List<Student>();
        
        var allStudents = new List<Student>();
        var response = _client.GetAsync($"{_client.BaseAddress}/Student/Get").Result;

        if (response.IsSuccessStatusCode)
        {
            var data = response.Content.ReadAsStringAsync().Result;
            allStudents = JsonConvert.DeserializeObject<List<Student>>(data);
            studentView.Students = allStudents;
        }

        return View(studentView);
    }

    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Add(StudentAddViewModel studentAddViewModel)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var student = new Student
                {
                    username = studentAddViewModel.Student.username,
                    password = studentAddViewModel.Student.password,
                    studentName = studentAddViewModel.Student.studentName,
                    studentLastName = studentAddViewModel.Student.studentLastName
                };

                var data = JsonConvert.SerializeObject(student);
                var content = new StringContent(data, Encoding.UTF8, "application/json");
                var response = _client.PostAsync($"{_client.BaseAddress}/Student/Post", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Student Created";
                    return RedirectToAction("Index");
                }
            }

            catch (Exception ex)
            {
                TempData["successMessage"] = ex.Message;
                return View();
            }

            return View(studentAddViewModel);
        }

        return View(studentAddViewModel);
    }

    public IActionResult Edit(ObjectId id)
    {
        if (id == null || id == ObjectId.Empty)
            return NotFound();

        Student? selectedStudent = null;
        var response = _client.GetAsync($"{_client.BaseAddress}/Student/GetID/{id}").Result;

        if (response.IsSuccessStatusCode)
        {
            var data = response.Content.ReadAsStringAsync().Result;
            selectedStudent = JsonConvert.DeserializeObject<Student>(data);
        }

        return View();
    }

    [HttpPost]
    public IActionResult Edit(StudentAddViewModel student)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var data = JsonConvert.SerializeObject(student.Student);
                var content = new StringContent(data, Encoding.UTF8, "aplpication/json");

                var response = _client.PutAsync($"{_client.BaseAddress}/Edit/{student.Student.Id}", content).Result;

                if (response.IsSuccessStatusCode)
                    return RedirectToAction("Index");

                return View();
            }
            
            return BadRequest();
        }

        catch (Exception ex)
        {
            ModelState.AddModelError("", $"Updating Student failed, Exception: {ex.Message}");
        }

        return View(student);
    }

    public IActionResult Delete(ObjectId id)
    {
        if (id == null || id == ObjectId.Empty)
            return NotFound();
        
        Student? selectedStudent = null;
        var response = _client.GetAsync($"{_client.BaseAddress}/Student/GetID/{id}").Result;

        if (response.IsSuccessStatusCode)
        {
            var data = response.Content.ReadAsStringAsync().Result;
            selectedStudent = JsonConvert.DeserializeObject<Student>(data);
        }

        return View(selectedStudent);
    }

    [HttpPost]
    public IActionResult Delete(Student student)
    {
        if (student.Id == ObjectId.Empty)
        {
            ViewData["ErrorMessage"] = "Deleting the student failed, invalid ID";
            return View();
        }

        try
        {
            var response = _client.DeleteAsync($"{_client.BaseAddress}/Student/Delete/{student.Id}").Result;

            if (response.IsSuccessStatusCode)
            {
                TempData["StudentDeleted"] = "Student deleted";
                return RedirectToAction("Index");
            }

            return View();
        }

        catch (Exception ex)
        {
            ViewData["ErrorMessage"] = $"Deleting student failed. Exception: {ex.Message}";
        }

        return View(student);
    }*/
}