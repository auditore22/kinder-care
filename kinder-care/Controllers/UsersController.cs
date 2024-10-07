using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using kinder_care.Models;
using Microsoft.AspNetCore.Authorization;

namespace kinder_care.Controllers;

[Authorize]
public class UsersController(ILogger<UsersController> logger) : Controller
{
    private readonly ILogger<UsersController> _logger = logger;

    public ActionResult ManageRoles()
    {
        ViewBag.CurrentSection = "ManageRoles";
        return RedirectToAction("Index", "Usuarios"); //Redirigir al controller de Usuarios
    }

    public ActionResult ManageProfiles()
    {
        ViewBag.CurrentSection = "ManageProfiles";
        return View();
    }

    public ActionResult ManageRecords()
    {
        ViewBag.CurrentSection = "ManageRecords";
        return View();
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}