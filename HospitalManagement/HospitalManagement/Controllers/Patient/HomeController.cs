using HospitalManagement.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Controllers.Patient
{
    public class HomeController : Controller
    {
        private readonly HospitalDbContext _hospitalDbContext;

        public HomeController()
        {
            _hospitalDbContext = new HospitalDbContext();
        }

        public ViewResult Index()
        {
            return View(_hospitalDbContext.Doctors.ToList());
        }

        public ViewResult Login()
        {
            return View();
        }

        public ViewResult Register()
        {
            return View();
        }

        public ViewResult Appointment()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            _hospitalDbContext.Dispose();
            base.Dispose(disposing);
        }
    }
}
