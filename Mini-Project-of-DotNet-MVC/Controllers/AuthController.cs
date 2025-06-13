using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mini_Project_of_DotNet_MVC.Models;
using System.Security.Claims;


namespace Mini_Project_of_DotNet_MVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        private readonly ApplicationContext _context;



        public AuthController(ApplicationContext context , IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
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



        public IActionResult Login()
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

                return RedirectToAction("Dashboard");

            }
            else
            {
                return RedirectToAction("login");
            }
        }

        public IActionResult Learner_license()
        {
            // Retrieve the session value for "key_userName"
            string fullName = HttpContext.Session.GetString("key_userName");

            // Check if the session value exists
            if (string.IsNullOrEmpty(fullName))
            {
                // Redirect to login if session is not set
                return RedirectToAction("Login", "Auth");
            }

            // Pass the session value to the view
           ViewBag.FullName = fullName;

            return View();
        }

        public IActionResult Dashboard()
        {
            // Retrieve the session value for "key_userName"
            string fullName = HttpContext.Session.GetString("key_userName");
            ViewBag.FullName = fullName;

            return View();
        }

      public IActionResult Category_vehicle()
        {

            return View();

        }

        //updated code start
        [HttpPost]
        public async Task<IActionResult> Category_vehicle(VehicleRegistration model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                // Define folder paths (folders already exist in wwwroot)
                string userPhotoFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Credentials/UserPhotos");
                string cnicFrontFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Credentials/CnicFronts");
                string cnicBackFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Credentials/CnicBacks");

                // Process User Photo
                if (model.UserPhoto == null)
                {
                    ModelState.AddModelError("UserPhoto", "Please upload a user photo");
                    return View(model);
                }
                string userPhotoPath = await ProcessUploadedFile(model.UserPhoto, userPhotoFolder);
                model.UserPhotoPath = userPhotoPath;

                // Process CNIC Front
                if (model.CnicFront == null)
                {
                    ModelState.AddModelError("CnicFront", "Please upload CNIC front photo");
                    return View(model);
                }
                string cnicFrontPath = await ProcessUploadedFile(model.CnicFront, cnicFrontFolder);
                model.CnicFrontPath = cnicFrontPath;

                // Process CNIC Back
                if (model.CnicBack == null)
                {
                    ModelState.AddModelError("CnicBack", "Please upload CNIC back photo");
                    return View(model);
                }
                string cnicBackPath = await ProcessUploadedFile(model.CnicBack, cnicBackFolder);
                model.CnicBackPath = cnicBackPath;

                // Set timestamps and save
                model.CreatedAt = DateTime.Now;
                model.ExpiryDate = DateTime.Now.AddYears(1);

                _context.VehicleRegistrations.Add(model);
                await _context.SaveChangesAsync();

                return RedirectToAction("Learner_license");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error occurred: {ex.Message}");
                return View(model);
            }
        }

        private async Task<string> ProcessUploadedFile(IFormFile file, string folderPath)
        {
            string validExtensions = ".jpg,.jpeg,.png,.gif";
            string fileExtension = Path.GetExtension(file.FileName).ToLower();

            if (!validExtensions.Contains(fileExtension))
            {
                throw new Exception("Only image files (JPG, PNG, GIF) are allowed");
            }

            string uniqueFileName = Guid.NewGuid().ToString() + fileExtension;
            string filePath = Path.Combine(folderPath, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return $"/Credentials/{Path.GetFileName(folderPath)}/{uniqueFileName}";
        }

        //updated code end



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
