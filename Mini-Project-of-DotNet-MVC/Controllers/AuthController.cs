using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Registration rge)
        {
            if (!ModelState.IsValid)
            {
                return View("Register", rge); // Fix spelling + pass model
            }

            _context.registrations.Add(rge);
            await _context.SaveChangesAsync();
            return RedirectToAction("Login"); // Also ensure "Login" is correct
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
                //return RedirectToAction("Index", "Template
             HttpContext.Session.SetString("key_userName", _login.FullName);

                return RedirectToAction("profile");

            }
            else
            {
                return RedirectToAction("login");
            }
        }

        public IActionResult profile()
        {
            // Retrieve the session value for "key1"
            string fullName = HttpContext.Session.GetString("key_userName");

            // Check if the session value exists
            if (string.IsNullOrEmpty(fullName))
            {
                // Optionally redirect to login if session is not set
                return RedirectToAction("login");
            }

            // Pass the session value to the view (e.g., via ViewBag or a model)
            ViewBag.FullName = fullName;

            return View();
        }

        public async Task<IActionResult> Logout()
        {
            // Clear the session data
            HttpContext.Session.Clear();

            // Sign out the user from authentication
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Prevent the browser from caching the page
            Response.Headers["Cache-Control"] = "no-store, no-cache, must-revalidate"; // Prevent caching
            Response.Headers["Pragma"] = "no-cache"; // For HTTP 1.0
            Response.Headers["Expires"] = "0"; // Expire immediately

            // Redirect to the Login page
            return RedirectToAction("login");
        }
    }
}
