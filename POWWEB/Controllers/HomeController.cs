using Microsoft.AspNetCore.Mvc;

namespace POWWEB.Controllers
{
    public class HomeController : Controller
    {
        // Acción para cargar la página principal
        public IActionResult Index()
        {
            // Simula datos dinámicos del usuario
            ViewData["UserName"] = User?.Identity?.Name ?? "Invitado";
            return View();
        }

        // Acción para manejar la sección "Inscripción de Cursos"
        public IActionResult InscripcionCursos()
        {
            // Simula una lista de cursos disponibles
            var cursos = new List<string> { "Matemáticas", "Historia", "Física", "Programación" };
            return Json(cursos);
        }

        // Acción para manejar la sección "Calificaciones"
        public IActionResult Calificaciones()
        {
            // Simula calificaciones del usuario
            var calificaciones = new Dictionary<string, string>
            {
                { "Matemáticas", "A" },
                { "Historia", "B+" },
                { "Física", "A-" },
                { "Programación", "A+" }
            };
            return Json(calificaciones);
        }

        // Acción para manejar la sección "Pagos"
        public IActionResult Pagos()
        {
            // Simula historial de pagos
            var pagos = new List<string> { "Pago 1: $100", "Pago 2: $200", "Pago 3: $150" };
            return Json(pagos);
        }

        // Acción para manejar la sección "Documentos"
        public IActionResult Documentos()
        {
            // Simula documentos disponibles
            var documentos = new List<string> { "Certificado de estudios", "Comprobante de inscripción" };
            return Json(documentos);
        }
    }
}
