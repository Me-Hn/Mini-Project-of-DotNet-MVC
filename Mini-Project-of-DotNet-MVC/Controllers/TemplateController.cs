using Microsoft.AspNetCore.Mvc;

namespace Mini_Project_of_DotNet_MVC.Controllers
{
    public class TemplateController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult about()
        {
            return View();
        }

        public IActionResult testomonial()
        {
            return View();
        }

        public IActionResult contact()
        {
            return View();
        }


    }
}
