using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class LoginLogic
{
    private readonly HttpClient _httpClient;

    public LoginLogic(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // Método para alternar entre mostrar y ocultar la contraseña
    public string TogglePasswordVisibility(string currentType)
    {
        return currentType == "password" ? "text" : "password";
    }

    // Método para manejar el envío del formulario
    public async Task<string> SubmitLoginAsync(string username, string password)
    {
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            return "Por favor, ingrese usuario y contraseña.";
        }

        try
        {
            // Crea el contenido de la solicitud
            var loginData = new { username, password };
            var content = new StringContent(JsonSerializer.Serialize(loginData), Encoding.UTF8, "application/json");

            // Envía la solicitud al servidor
            var response = await _httpClient.PostAsync("/api/auth/login", content);

            // Procesa la respuesta
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<LoginResponse>(responseBody);

                if (result != null && result.Success)
                {
                    return "success"; // Indica que la autenticación fue exitosa
                }
                else
                {
                    return "Credenciales incorrectas, inténtalo de nuevo.";
                }
            }
            else
            {
                return "Ocurrió un error al intentar iniciar sesión. Por favor, inténtalo más tarde.";
            }
        }
        catch (Exception ex)
        {
            // Maneja errores en la solicitud
            Console.WriteLine($"Error al autenticar: {ex.Message}");
            return "Ocurrió un error al intentar iniciar sesión. Por favor, inténtalo más tarde.";
        }
    }

    // Clase para deserializar la respuesta del servidor
    private class LoginResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
