using HospitalManagement.Database;
using HospitalManagement.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
           // DatabaseService databaseService = new DatabaseService();
            //databaseService.InitializeTables();
            //databaseService.Dispose();

            var builder = WebApplication.CreateBuilder(args);
            builder.Services
                .AddControllersWithViews()
                .AddRazorRuntimeCompilation();

            builder.Services
                .AddAuthentication("Cookies")
                .AddCookie("Cookies");


            var app = builder.Build();


            app.UseAuthentication();
            app.UseAuthorization();

            app.UseStaticFiles();

            //app.MapGet("/", () => "Hello World!");
            app.MapControllerRoute("default", "{controller=Home}/{action=Index}");

            app.Run();
        }
    }


    
}