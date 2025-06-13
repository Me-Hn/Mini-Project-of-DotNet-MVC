using Microsoft.EntityFrameworkCore;
using Mini_Project_of_DotNet_MVC.Models;

namespace Mini_Project_of_DotNet_MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //session work
            builder.Services.AddDistributedMemoryCache();

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(50);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            //session work

            //logout work
            builder.Services.AddAuthentication("Cookies") // "Cookies" is the default scheme name
            .AddCookie("Cookies", options =>
            {
                options.LoginPath = "/auth/login"; // Set your login path
                options.LogoutPath = "/auth/Logout"; // Set your logout path
                options.AccessDeniedPath = "/auth/AccessDenied"; // Set your access denied path
            });

            //logout work
            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<ApplicationContext>(item =>
            item.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            //session
            //builder.Services.AddDistributedMemoryCache();

            //builder.Services.AddSession(options =>
            //{
            //    options.IdleTimeout = TimeSpan.FromSeconds(10);
            //    options.Cookie.HttpOnly = true;
            //    options.Cookie.IsEssential = true;
            //});

            //session
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            //use session

            app.UseSession();

            //use session
            //use logout

            app.Use(async (context, next) =>
            {
                context.Response.Headers["Cache-Control"] = "no-store, no-cache, must-revalidate"; // HTTP 1.1.
                context.Response.Headers["Pragma"] = "no-cache"; // For HTTP 1.0.
                context.Response.Headers["Expires"] = "0"; // Expire immediately
                await next();
            });

            //use logout
            app.UseAuthorization();

            app.UseSession();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Template}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
