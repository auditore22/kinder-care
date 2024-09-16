using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using kinder_care.Models;

namespace kinder_care.Controllers;

public class PaymentsController(ILogger<PaymentsController> logger) : Controller
{
    private readonly ILogger<PaymentsController> _logger = logger;

    public ActionResult ManagePayments()
    {
        ViewBag.CurrentSection = "ManagePayments";
        return View();
    }

    public ActionResult FinanceDetails()
    {
        ViewBag.CurrentSection = "FinanceDetails";
        return View();
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}