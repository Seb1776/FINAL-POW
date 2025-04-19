using Microsoft.AspNetCore.Mvc;
using System.Linq;

public class AccountController : Controller
{
    private readonly AppDbContext _context;

    // Inyección de dependencias
    public AccountController(AppDbContext context)
    {
        _context = context;
    }

    // Método para obtener el correo electrónico desde la base de datos
    public string GetUserEmailById(int userId)
    {
        // Busca el usuario en la base de datos
        var user = _context.Users.FirstOrDefault(u => u.Id == userId);

        if (user == null)
        {
            // Si no se encuentra el usuario
            return null;
        }

        // Devuelve el correo electrónico
        return user.Email;
    }
}
