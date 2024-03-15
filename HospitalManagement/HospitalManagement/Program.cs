using HospitalManagement.Contracts;
using HospitalManagement.Database;
using HospitalManagement.Services;
using HospitalManagement.Services.Abstract;
using HospitalManagement.Services.Concrets;
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


            builder.Services
                .AddScoped<IUserService, UserService>()
                .AddSingleton<IFileService, IFileService>()
                .AddDbContext<HospitalDbContext>(o =>
                {
                    o.UseNpgsql(DatabaseConstants.CONNECTION_STRING);
                });

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