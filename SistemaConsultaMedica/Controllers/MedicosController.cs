using Microsoft.AspNetCore.Mvc;

namespace SistemaConsultaMedica.Controllers;

public class MedicosController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}