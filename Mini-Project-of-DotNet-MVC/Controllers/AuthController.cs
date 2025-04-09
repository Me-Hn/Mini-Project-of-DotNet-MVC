using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Mini_Project_of_DotNet_MVC.Models;

namespace Mini_Project_of_DotNet_MVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly ApplicationContext _context;

        public AuthController(ApplicationContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(Registration rge)
        {
            var _data = _context.registrations.Add(rge);
            await _context.SaveChangesAsync();
            return RedirectToAction("login");
        }


       
        public IActionResult login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> login(Registration log)
        {
            var _login = _context.registrations.Where(x => (x.Email == log.Email || x.CNICNumber == log.CNICNumber) && x.Password == log.Password).FirstOrDefault();

            if (_login != null)
            {
                return RedirectToAction("Index", "Template");
            }
            else
            {
                return RedirectToAction("login");
            }


        }
    }
}
