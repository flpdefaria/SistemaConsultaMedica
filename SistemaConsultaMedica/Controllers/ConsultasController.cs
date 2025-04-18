using Microsoft.AspNetCore.Mvc;

namespace SistemaConsultaMedica.Controllers;

public class ConsultasController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}