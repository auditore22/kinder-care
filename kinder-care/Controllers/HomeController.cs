using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using kinder_care.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace kinder_care.Controllers;

[Authorize]
public class HomeController(ILogger<HomeController> logger) : Controller
{
    private readonly KinderCareContext _context;
    private readonly ILogger<HomeController> _logger = logger;

    public IActionResult Index()
    {
        var currentUserId = User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).SingleOrDefault();
        int parsedCurrentUserId = int.TryParse(currentUserId, out int result) ? result : -1; // Convierte a entero o usa un valor no válido (-1) si falla

        return View();
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

}