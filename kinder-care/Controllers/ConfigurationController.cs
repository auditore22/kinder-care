using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using kinder_care.Models;

namespace kinder_care.Controllers;

public class ConfigurationController(ILogger<ConfigurationController> logger) : Controller
{
    private readonly ILogger<ConfigurationController> _logger = logger;

    public ActionResult SystemConfig()
    {
        ViewBag.CurrentSection = "SystemConfig";
        return View();
    }

    public ActionResult UserProfile()
    {
        ViewBag.CurrentSection = "UserProfile";
        return View();
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}