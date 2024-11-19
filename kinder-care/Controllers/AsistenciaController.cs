using kinder_care.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace kinder_care.Controllers
{
    public class AsistenciaController : Controller
    {
        private readonly KinderCareContext _context;

        public AsistenciaController(KinderCareContext context)
        {
            _context = context;
        }
        //======================================================[VISTA INDEX]==========================================================================================
        public async Task<IActionResult> ListaNinos()
        {
            var ninos = await _context.Ninos.ToListAsync();

            return View(ninos);
        }

        public IActionResult ListaAsistencia()
        {
            var asistencias = _context.Asistencia
                .Include(n => n.IdNinoNavigation)
                .ToList();  // Obtener lista de Ninos
            return View(asistencias);  // Pasar la lista a la vista
        }

        [HttpPost]
        public IActionResult RegistrarAsistencia(List<int> selectedNinos)
        {
            if (selectedNinos != null && selectedNinos.Any())
            {
                foreach (var idNino in selectedNinos)
                {
                    var asistencias = new Asistencia
                    {
                        IdNino = idNino,
                        Fecha = DateTime.Now,
                        HoraEntrada = DateTime.Now, // Hora actual de entrada
                        HoraSalida = null,
                        Estado = "Presente"
                    };

                    _context.Asistencia.Add(asistencias);  // Añadir a la tabla de Asistencia
                }

                _context.SaveChanges();  // Guardar cambios en la base de datos
            }

            return RedirectToAction("ListaAsistencia");  // Volver a mostrar la lista de niños
        }

    }
}
