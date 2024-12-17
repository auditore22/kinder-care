using kinder_care.Models;
using kinder_care.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Security.Claims;


namespace kinder_care.Controllers
{
    public class AsistenciaController : Controller
    {
        private readonly KinderCareContext _context;

        public AsistenciaController(KinderCareContext context)
        {
            _context = context;
        }

        //===========================[Relacion Ninos y Docentes]===========================
        public async Task<IActionResult> ListaNinos()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var userRole = User.FindFirstValue(ClaimTypes.Role);
            var docente = await _context.Docentes.FirstOrDefaultAsync(d => d.IdUsuario == userId);

            ViewBag.DocenteId = docente.IdDocente;
            
            if (userRole == "Docente")
            {
                var estudiantes = await _context.RelDocenteNinoMateria
                    .Where(r => r.IdDocente == docente!.IdDocente)
                    .Select(r => r.IdNino)
                    .Join(_context.Ninos,
                        rel => rel,
                        nino => nino.IdNino,
                        (rel, nino) => nino)
                    .ToListAsync();
                
                if (!estudiantes.Any())
                {
                    ViewBag.Mensaje = "No hay estudiantes relacionados con este docente.";
                    return View(new List<Ninos>());
                }
                return View(estudiantes);
            }
            return View(new List<Ninos>());
        }

        public async Task<IActionResult> ListaNinos_Administracion()
        {
            var ninos = await _context.Ninos
                .Include(r => r.RelDocenteNinoMateria)
                .ThenInclude(d => d.IdDocenteNavigation)
                .ThenInclude(u => u.IdUsuarioNavigation)
                .ToListAsync();
            return View(ninos);
        }

        [HttpGet]
        public async Task<IActionResult> CrearGrupo()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var docente = await _context.Docentes
                .Include(d => d.IdUsuarioNavigation)
                .FirstOrDefaultAsync(d => d.IdUsuario == userId);

            var estudiantesDisponibles = await _context.Ninos
                .Where(n => !_context.RelDocenteNinoMateria
                .Any(r => r.IdNino == n.IdNino))
                .ToListAsync();

            var vm = new RelacionDocenteNinosVM
            {
                IdDocente = docente.IdDocente,
                UsuarioNombre = docente.IdUsuarioNavigation.Nombre,
                UsuarioCedula = docente.IdUsuarioNavigation.Cedula,
                Ninos = estudiantesDisponibles
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> CrearGrupo(List<int> Ninos)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var docente = await _context.Docentes.FirstOrDefaultAsync(d => d.IdUsuario == userId);

            foreach (var ninoId in Ninos)
            {
                if (!_context.RelDocenteNinoMateria.Any(r => r.IdDocente == docente.IdDocente && r.IdNino == ninoId))
                {
                    _context.RelDocenteNinoMateria.Add(new RelDocenteNinoMateria
                    {
                        IdDocente = docente.IdDocente,
                        IdNino = ninoId
                    });
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("ListaNinos");
        }

        //===========================[Asistencia de Estudiantes]===========================
        public async Task<IActionResult> ControlAsistencias()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var userRole = User.FindFirstValue(ClaimTypes.Role);
            var docente = await _context.Docentes.FirstOrDefaultAsync(d => d.IdUsuario == userId);

            if (userRole == "Docente")
            {
                var estudiantes = await _context.RelDocenteNinoMateria
                    .Where(r => r.IdDocente == docente.IdDocente)
                    .Select(r => r.IdNino)
                    .Join(_context.Ninos,
                        rel => rel,
                        nino => nino.IdNino,
                        (rel, nino) => nino)
                    .ToListAsync();

                if (!estudiantes.Any())
                {
                    ViewBag.Mensaje = "No se pueden hacer asistencias por que no hay estudiantes en el grupo";
                    return View();
                }
                return View(estudiantes);
            }
            return View(new List<Ninos>());
        }

        [HttpPost]
        public async Task<IActionResult> ControlAsistencias(List<int> estudiantesPresentes)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var fechaActual = DateTime.Now;
            var docente = await _context.Docentes.FirstOrDefaultAsync(d => d.IdUsuario == userId);

            var estudiantes = await _context.RelDocenteNinoMateria
                .Where(r => r.IdDocente == docente.IdDocente)
                .Select(r => r.IdNino)
                .ToListAsync();

            foreach (var idNino in estudiantes)
            {
                var presente = estudiantesPresentes?.Contains(idNino) ?? false;

                var asistencia = new Asistencia
                {
                    IdNino = idNino,
                    Fecha = fechaActual,
                    Presente = presente 
                };

                _context.Asistencia.Add(asistencia);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("ListaNinos");
        }

        public IActionResult ListaAsistencia(DateTime? fechaInicio, DateTime? fechaFin)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var docente = _context.Docentes.FirstOrDefault(d => d.IdUsuario == userId);
            
            var estudiantes = _context.RelDocenteNinoMateria
                .Where(r => r.IdDocente == docente.IdDocente)
                .Select(r => r.IdNino)
                .ToList();

            var query = _context.Asistencia
                .Include(a => a.IdNinoNavigation)
                .Where(a => estudiantes.Contains(a.IdNino)) 
                .AsQueryable();


            if (fechaInicio.HasValue && fechaFin.HasValue)
            {
                query = query.Where(a => a.Fecha >= fechaInicio.Value && a.Fecha <= fechaFin.Value);
            }

            var asistencias = query
                .OrderByDescending(a => a.Fecha)
                .ToList();

            ViewBag.FechaInicio = fechaInicio?.ToString("yyyy-MM-dd");
            ViewBag.FechaFin = fechaFin?.ToString("yyyy-MM-dd");

            return View(asistencias);
        }

        public IActionResult ListaAsistencia_Admin(DateTime? fechaInicio, DateTime? fechaFin)
        {
            var query = _context.Asistencia
                    .Include(a => a.IdNinoNavigation)
                    .AsQueryable();


            if (fechaInicio.HasValue && fechaFin.HasValue)
            {
                query = query.Where(a => a.Fecha >= fechaInicio.Value && a.Fecha <= fechaFin.Value);
            }

            var asistencias = query
                .OrderByDescending(a => a.Fecha)
                .ToList();

            ViewBag.FechaInicio = fechaInicio?.ToString("yyyy-MM-dd");
            ViewBag.FechaFin = fechaFin?.ToString("yyyy-MM-dd");

            return View(asistencias);
        }

        //=============================[Reporte de Asistencia]=============================
        public IActionResult GenerarReportePDF(DateTime? fechaInicio, DateTime? fechaFin)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var docente = _context.Docentes
                .Include(d => d.IdUsuarioNavigation)
                .FirstOrDefault(d => d.IdUsuario == userId);

            var estudiantes = _context.RelDocenteNinoMateria
                .Where(r => r.IdDocente == docente.IdDocente)
                .Select(r => r.IdNino)
                .ToList();

            var query = _context.Asistencia
                .Include(a => a.IdNinoNavigation)
                .Where(a => estudiantes.Contains(a.IdNino))
                .AsQueryable();

            if (fechaInicio.HasValue && fechaFin.HasValue)
            {
                query = query.Where(a => a.Fecha >= fechaInicio.Value && a.Fecha <= fechaFin.Value);
            }

            var asistencias = query.OrderByDescending(a => a.Fecha).ToList();

            var PdfDoc = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.DefaultTextStyle(TextStyle.Default.Size(12));
                    page.Header().Text($"Reporte de Asistencias - {docente.IdUsuarioNavigation.Nombre}").Bold().FontSize(18).AlignCenter();
                    page.Content().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(100); 
                            columns.RelativeColumn();   
                            columns.ConstantColumn(80);  
                        });

                        table.Header(header =>
                        {
                            header.Cell().Text("Fecha").Bold();
                            header.Cell().Text("Estudiante").Bold();
                            header.Cell().Text("Estado").Bold();
                        });

                        foreach (var asistencia in asistencias)
                        {
                            table.Cell().Text(asistencia.Fecha.ToString("dd/MM/yyyy"));
                            table.Cell().Text(asistencia.IdNinoNavigation.NombreNino);
                            table.Cell().Text(asistencia.Presente ? "Presente" : "Ausente")
                                 .FontColor(asistencia.Presente ? Colors.Green.Darken2 : Colors.Red.Darken2);
                        }
                    });

                    page.Footer().AlignCenter().Text(text =>
                    {
                        text.Span("Página ");
                        text.CurrentPageNumber();
                        text.Span(" de ");
                        text.TotalPages();
                    });
                });
            });

            var pdfBytes = PdfDoc.GeneratePdf();
            return File(pdfBytes, "application/pdf", "Reporte_Asistencia.pdf");
        }

        //=============================[Eliminar de Grupos]=============================
        [HttpPost]
        public async Task<IActionResult> EliminarRelacion(int idNino)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var docente = await _context.Docentes.FirstOrDefaultAsync(d => d.IdUsuario == userId);

            var relacion = await _context.RelDocenteNinoMateria
                .FirstOrDefaultAsync(r => r.IdDocente == docente.IdDocente && r.IdNino == idNino);

            _context.RelDocenteNinoMateria.Remove(relacion);
            await _context.SaveChangesAsync();

            ViewBag.Mensaje = "El estudiante fue eliminado del grupo.";
            return RedirectToAction("ListaNinos");
        }
    }
}
