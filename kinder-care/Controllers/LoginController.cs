using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using kinder_care.Models;

namespace kinder_care.Controllers;

public class LoginController(ILogger<LoginController> logger) : Controller
{
    private readonly ILogger<LoginController> _logger = logger;

    public IActionResult Index()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}