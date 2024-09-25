using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using kinder_care.Models;
using Microsoft.AspNetCore.Authorization;

namespace kinder_care.Controllers;

[Authorize]
public class EnrollmentController(ILogger<EnrollmentController> logger) : Controller
{
    private readonly ILogger<EnrollmentController> _logger = logger;

    public ActionResult ManageEnrollments()
    {
        ViewBag.CurrentSection = "ManageEnrollments";
        return View();
    }

    public ActionResult StudentProfile()
    {
        ViewBag.CurrentSection = "StudentProfile";
        return View();
    }

    public ActionResult AcademicRecord()
    {
        ViewBag.CurrentSection = "AcademicRecord";
        return View();
    }

    public ActionResult AttendanceRecord()
    {
        ViewBag.CurrentSection = "AttendanceRecord";
        return View();
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}