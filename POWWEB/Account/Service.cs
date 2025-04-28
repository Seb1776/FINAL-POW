using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

public class AuthService
{
    private readonly string _connectionString;

    public AuthService(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public string ValidateUser(string username, string password)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            // Consulta para validar al usuario en todas las tablas
            string query = @"
                SELECT 'student' AS role FROM student WHERE username = @username AND password = @password
                UNION
                SELECT 'professor' AS role FROM professor WHERE username = @username AND password = @password
                UNION
                SELECT 'admin' AS role FROM admin WHERE username = @username AND password = @password";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@password", password); // Recuerda que las contraseñas deben estar encriptadas

            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    return reader["role"].ToString(); // Retorna el rol del usuario (student, professor o admin)
                }
            }
        }

        return null; // Devuelve null si las credenciales son incorrectas
    }
}
