using Microsoft.AspNetCore.Mvc;

namespace KForceCars.Controllers;

public class CarsController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}