using System;
using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Mvc;

public class AccountController : Controller
{
    // GET: Mostrar el formulario para ingresar el correo
    public IActionResult ForgotPassword()
    {
        return View();
    }

    // POST: Procesar la solicitud de recuperación
    [HttpPost]
    public IActionResult ForgotPassword(string email)
    {
        // Validar que el correo exista en la base de datos (simulado aquí)
        var user = GetUserByEmail(email); // Implementa esta lógica según tu proyecto
        if (user == null)
        {
            // Enviar un mensaje de error si el correo no está registrado
            ModelState.AddModelError("", "El correo no está registrado.");
            return View();
        }

        // Generar un token único para la recuperación
        var resetToken = Guid.NewGuid().ToString();

        // Guardar el token en la base de datos (simulado aquí)
        SavePasswordResetToken(user.Id, resetToken); // Implementa esta lógica según tu proyecto

        // Crear un enlace de recuperación (ajusta la URL según tu proyecto)
        var resetLink = Url.Action("ResetPassword", "Account", new { token = resetToken }, Request.Scheme);

        // Enviar el correo electrónico
        SendRecoveryEmail(email, resetLink);

        // Mostrar un mensaje de confirmación
        ViewBag.Message = "Se ha enviado un enlace de recuperación a tu correo.";
        return View();
    }

    // GET: Mostrar el formulario para restablecer la contraseña
    public IActionResult ResetPassword(string token)
    {
        // Validar el token (simulado aquí)
        if (!IsTokenValid(token)) // Implementa esta lógica según tu proyecto
        {
            return BadRequest("Token inválido o expirado.");
        }

        // Mostrar el formulario para restablecer la contraseña
        ViewBag.Token = token;
        return View();
    }

    // POST: Procesar el restablecimiento de contraseña
    [HttpPost]
    public IActionResult ResetPassword(string token, string newPassword)
    {
        // Validar el token y actualizar la contraseña
        var userId = GetUserIdByToken(token); // Implementa esta lógica según tu proyecto
        if (userId == null)
        {
            return BadRequest("Token inválido o expirado.");
        }

        // Actualizar la contraseña del usuario
        UpdateUserPassword(userId, newPassword); // Implementa esta lógica según tu proyecto

        // Eliminar el token de la base de datos (opcional)
        DeletePasswordResetToken(token); // Implementa esta lógica según tu proyecto

        // Redirigir al usuario
        return RedirectToAction("Login", "Account");
    }

    // Método para enviar el correo de recuperación
    private void SendRecoveryEmail(string email, string resetLink)
    {
        var smtpClient = new SmtpClient("smtp.example.com") // Cambia por tu servidor SMTP
        {
            Port = 587,
            Credentials = new NetworkCredential("tu-correo@example.com", "tu-contraseña"),
            EnableSsl = true,
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress("tu-correo@example.com"),
            Subject = "Recuperación de contraseña",
            Body = $"<p>Haz clic en el siguiente enlace para restablecer tu contraseña:</p><a href='{resetLink}'>Restablecer contraseña</a>",
            IsBodyHtml = true,
        };
        mailMessage.To.Add(email);

        smtpClient.Send(mailMessage);
    }
}
