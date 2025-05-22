using POWAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace WebApplication1.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class OperationsController : Controller
{
    private IMongoCollection<User> _users;
    private UserManager<User> userManager;

    public OperationsController(MongoService mongo, UserManager<User> userManager)
    {
        _users = mongo.UserCollection;
        this.userManager = userManager;
    }

    [HttpPost]
    public async Task<IActionResult> Create(User user)
    {
        if (ModelState.IsValid)
        {
            var appUser = new User
            {
                UserName = user.Name,
                Email = user.Email
            };

            var result = await userManager.CreateAsync(appUser, user.Password);

            if (result.Succeeded)
                ViewBag.Message = "User Created Successfully";

            else
            {
                foreach (var err in result.Errors)
                    ModelState.AddModelError("", err.Description);
            }
        }

        return View(user);
    }

    [HttpGet]
    public async Task<IEnumerable<User>> Get()
    {
        return await _users.Find(FilterDefinition<User>.Empty).ToListAsync();
    }
}