// Obtén los elementos del formulario y campos
const loginForm = document.getElementById('loginForm');
const errorMessage = document.getElementById('error-message');
const togglePassword = document.getElementById('togglePassword');
const passwordField = document.getElementById('password');

// Alternar entre mostrar y ocultar la contraseña
togglePassword.addEventListener('click', () => {
    const type = passwordField.type === 'password' ? 'text' : 'password';
    passwordField.type = type;
    togglePassword.textContent = type === 'password' ? '👁️' : '🙈'; // Cambia el ícono
});

// Manejar el envío del formulario
loginForm.addEventListener('submit', async (e) => {
    e.preventDefault(); // Evita el comportamiento predeterminado del formulario

    // Obtén los valores ingresados por el usuario
    const username = document.getElementById('username').value;
    const password = document.getElementById('password').value;

    try {
        // Envía la solicitud al servidor
        const response = await axios.post('/api/auth/login', { username, password });

        if (response.data.success) {
            // Si la autenticación es exitosa, redirige al usuario
            window.location.href = '/home';
        } else {
            // Muestra un mensaje de error si las credenciales no son válidas
            errorMessage.style.display = 'block';
            errorMessage.textContent = 'Credenciales incorrectas, inténtalo de nuevo.';
        }
    } catch (error) {
        // Maneja errores en la solicitud
        errorMessage.style.display = 'block';
        errorMessage.textContent = 'Ocurrió un error al intentar iniciar sesión. Por favor, inténtalo más tarde.';
        console.error('Error al autenticar:', error);
    }
});
