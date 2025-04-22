using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace POWWEB.Controllers
{
    public class LoginController : Controller
    {
        private readonly HttpClient _httpClient;

        public LoginController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Acción para alternar la visibilidad de la contraseña
        [HttpPost]
        public JsonResult TogglePasswordVisibility([FromBody] string currentType)
        {
            var newType = currentType == "password" ? "text" : "password";
            return Json(new { newType });
        }

        // Acción para manejar el envío del formulario de inicio de sesión
        [HttpPost]
        public async Task<IActionResult> SubmitLoginAsync([FromForm] string username, [FromForm] string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return BadRequest("Por favor, ingrese usuario y contraseña.");
            }

            try
            {
                // Crear el contenido de la solicitud
                var loginData = new { username, password };
                var content = new StringContent(JsonSerializer.Serialize(loginData), Encoding.UTF8, "application/json");

                // Enviar la solicitud al servidor
                var response = await _httpClient.PostAsync("/api/auth/login", content);

                // Procesar la respuesta
                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<LoginResponse>(responseBody);

                    if (result != null && result.Success)
                    {
                        return Ok("success"); // Indica que la autenticación fue exitosa
                    }
                    else
                    {
                        return Unauthorized("Credenciales incorrectas, inténtalo de nuevo.");
                    }
                }
                else
                {
                    return StatusCode((int)response.StatusCode, "Ocurrió un error al intentar iniciar sesión. Por favor, inténtalo más tarde.");
                }
            }
            catch (Exception ex)
            {
                // Manejar errores en la solicitud
                Console.WriteLine($"Error al autenticar: {ex.Message}");
                return StatusCode(500, "Ocurrió un error al intentar iniciar sesión. Por favor, inténtalo más tarde.");
            }
        }

        // Clase para deserializar la respuesta del servidor
        private class LoginResponse
        {
            public bool Success { get; set; }
            public string Message { get; set; }
_

