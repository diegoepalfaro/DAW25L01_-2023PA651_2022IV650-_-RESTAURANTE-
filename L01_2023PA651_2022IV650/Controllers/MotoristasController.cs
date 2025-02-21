using Microsoft.AspNetCore.Mvc;

namespace L01_2023PA651_2022IV650.Controllers
{
    public class MotoristasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
