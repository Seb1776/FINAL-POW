using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using POWWEB.Models;

namespace POWWEB.Controllers;

public class StudentController : Controller
{
    private Uri _baseAddress = new Uri("https://localhost:7121/api");
    private readonly HttpClient _client;

    public StudentController()
    {
        _client = new HttpClient();
        _client.BaseAddress = _baseAddress;
    }
    
    // GET
    public IActionResult Index()
    {
        var studentList = new List<StudentViewModel>();
        var response = _client.GetAsync($"{_client.BaseAddress}/Student/Get").Result;

        if (response.IsSuccessStatusCode)
        {
            var data = response.Content.ReadAsStringAsync().Result;
            studentList = JsonConvert.DeserializeObject<List<StudentViewModel>>(data);
            Console.WriteLine($"{studentList?[0].Id} {studentList?[0].FirstName} {studentList?[0].LastName}");
        }
        
        return View(studentList);
    }
}