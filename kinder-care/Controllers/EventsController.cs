using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using kinder_care.Models;

namespace kinder_care.Controllers;

public class EventsController(ILogger<EventsController> logger) : Controller
{
    private readonly ILogger<EventsController> _logger = logger;

    public ActionResult Calendar()
    {
        ViewBag.CurrentSection = "Calendar";
        return View();
    }

    public ActionResult ManageEvents()
    {
        ViewBag.CurrentSection = "ManageEvents";
        return View();
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}