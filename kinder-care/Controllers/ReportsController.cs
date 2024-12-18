using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using kinder_care.Models;
using Microsoft.AspNetCore.Authorization;

namespace kinder_care.Controllers;

[Authorize(Roles = "Administrador, Docente")]
public class ReportsController(ILogger<ReportsController> logger) : Controller
{
    private readonly ILogger<ReportsController> _logger = logger;

    public ActionResult GenerateReports()
    {
        ViewBag.CurrentSection = "GenerateReports";
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}