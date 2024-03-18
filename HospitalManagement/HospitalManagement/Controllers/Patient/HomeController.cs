using HospitalManagement.Contracts;
using HospitalManagement.Database;
using HospitalManagement.Database.DomainModel;
using HospitalManagement.Services.Abstract;
using HospitalManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace HospitalManagement.Controllers.Patient
{
    public class HomeController : Controller
    {
        private readonly HospitalDbContext _dbContext;

        public HomeController(HospitalDbContext context)
        {
            _dbContext = context;
        }

        public ViewResult Index()
        {
            return View(_dbContext.Doctors.ToList());
        }

        protected override void Dispose(bool disposing)
        {
            _dbContext.Dispose();
            base.Dispose(disposing);
        }
    }
}
