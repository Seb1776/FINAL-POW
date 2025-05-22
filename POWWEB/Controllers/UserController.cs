using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using POWWEB.Models;

namespace POWWEB.Controllers;

public class UserController : Controller
{
    private Uri _baseAddress = new Uri("mongodb+srv://sebastianwho1776:lakezurich123@powcluster.vgn3vfq.mongodb.net/?retryWrites=true&w=majority&appName=POWCluster");
    private readonly HttpClient _client;

    public UserController()
    {
        _client = new HttpClient();
        _client.BaseAddress = _baseAddress;
    }
    
    // GET
    public IActionResult Index()
    {
        var userList = new List<UserViewModel>();
        var response = _client.GetAsync($"{_client.BaseAddress}/Operations/Get").Result;

        if (response.IsSuccessStatusCode)
        {
            var data = response.Content.ReadAsStringAsync().Result;
            userList = JsonConvert.DeserializeObject<List<UserViewModel>>(data);
        }
        
        return View(userList);
    }
}