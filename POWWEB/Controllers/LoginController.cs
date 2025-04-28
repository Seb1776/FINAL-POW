using Microsoft.AspNetCore.Mvc;

public class LoginController : Controller
{
    private readonly AuthService _authService;

    public LoginController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    public IActionResult Login(string username, string password)
    {
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            ViewBag.ErrorMessage = "Por favor, ingrese usuario y contraseña.";
            return View();
        }

        string role = _authService.ValidateUser(username, password);

        if (role != null)
        {
            // Login exitoso: redirigir según el rol
            if (role == "student")
                return RedirectToAction("Dashboard", "Student");
            else if (role == "professor")
                return RedirectToAction("Dashboard", "Professor");
            else if (role == "admin")
                return RedirectToAction("Dashboard", "Admin");
        }

        ViewBag.ErrorMessage = "Usuario o contraseña incorrectos.";
        return View();
    }
}
