using HospitalManagement.Contracts;
using HospitalManagement.Database;
using HospitalManagement.Database.DomainModel;
using HospitalManagement.Services.Abstract;
using HospitalManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Numerics;
using System.Reflection;

namespace HospitalManagement.Controllers.Admin
{
    [Route("admin/doctors")]
    public class DoctorController : Controller
    {
        private readonly HospitalDbContext _hospitalDbContext;
        private readonly ILogger<DoctorController> _logger;
        private readonly IFileService _fileService;

        public DoctorController(
            HospitalDbContext hospitalDbContext,
            ILogger<DoctorController> logger,
            IFileService fileService)
        {
            _hospitalDbContext = hospitalDbContext;
            _logger = logger;
            _fileService = fileService;
        }


        [HttpGet]
        public IActionResult Doctors()
        {
            var doctors = _hospitalDbContext.Doctors
                .Include(d => d.Department)
                .ToList();

            return View("Views/Admin/Doctor/Doctors.cshtml", doctors);
        }
       

        [HttpGet("add")]
        public IActionResult Add()
        {
            var model = new DoctorAddResponseViewModel
            {
                Departments = _hospitalDbContext.Departments.ToList(),
            };

            return View("Views/Admin/Doctor/DoctorAdd.cshtml", model);
        }

        [HttpPost("add")]
        public IActionResult Add(DoctorAddRequestViewModel model)
        {
            if (!ModelState.IsValid)
                return PrepareValidationView("Views/Admin/Doctor/DoctorAdd.cshtml");

            if (model.DepartmentId != null);
                {
                    var department = _hospitalDbContext.Departments.FirstOrDefault(d => d.Id == model.DepartmentId);
                    if (department == null)
                    {
                        ModelState.AddModelError("DepartmentId", "Department doesn't exist");

                        return PrepareValidationView("Views/Admin/Doctor/DoctorAdd.cshtml");
                    }
                }

            
                var doctor = new Doctor
                {
                    Name = model.Name,
                    Surname = model.Surname,
                    DepartmentId = model.DepartmentId,
                };
            try
            {
                _hospitalDbContext.Doctors.Add(doctor);
                _hospitalDbContext.SaveChanges();

            }
            catch (PostgresException e)
            {
                _logger.LogError(e, "Postgresql Exception");

                 throw e;
            }

            return RedirectToAction("Doctors");
        }

      
       
        [HttpGet("edit")]
        public IActionResult Edit(int id)
        {
            Doctor doctor = _hospitalDbContext.Doctors
                         .FirstOrDefault(d => d.Id == id);


            if (doctor == null) 
            {
                return NotFound();
            }

            var model = new DoctorUpdateResponseViewModel
            {
                Id = doctor.Id,
                Name = doctor.Name,
                Surname = doctor.Surname,
                Departments = _hospitalDbContext.Departments.ToList(),
                DepartmentId = doctor.DepartmentId,
            };

            return View("Views/Admin/Doctor/DoctorEdit.cshtml", model);
        }

        [HttpPost("edit")]
        public IActionResult Edit(DoctorUpdateRequestViewModel model)
        {
            if (!ModelState.IsValid)
                return View("Views/Admin/Doctor/DoctorEdit.cshtml");

            if (model.DepartmentId != null)
            {
                var department = _hospitalDbContext.Departments.FirstOrDefault(d => d.Id == model.DepartmentId);
                if (department == null)
                {
                    ModelState.AddModelError("DepartmentId", "Department doesn't exist");

                    return PrepareValidationView("Views/Admin/Doctor/DoctorEdit.cshtml");
                }
            }

            Doctor doctor = _hospitalDbContext.Doctors
                .FirstOrDefault(d => d.Id == model.Id);

            if (doctor == null)
                return NotFound();

            
                doctor.Name = model.Name;
                doctor.Surname = model.Surname;
                doctor.DepartmentId = model.DepartmentId;
            try
            {
                _hospitalDbContext.Doctors.Update(doctor);
                _hospitalDbContext.SaveChanges();
            }
            catch (PostgresException e)
            {
                _logger.LogError(e, "Postgresql Exception");
            throw e;
            }

            return RedirectToAction("Doctors");
        }

        

        [HttpGet("delete")]
        public IActionResult Delete(int id)
        {
            Doctor doctor = _hospitalDbContext.Doctors
                .FirstOrDefault (d => d.Id == id);

            if (doctor == null)
            {
                return NotFound();
            }

            _hospitalDbContext.Remove(doctor);
            _hospitalDbContext.SaveChanges();
            
            return RedirectToAction("Doctors");
        }

      private IActionResult PrepareValidationView (string viewName)
        {
            var responseViewModel = new DoctorAddResponseViewModel
            {
                Departments = _hospitalDbContext.Departments.ToList(),
            };

            return View(viewName, responseViewModel);
        }

        protected override void Dispose(bool disposing)
        {
            _hospitalDbContext.Dispose();
            base.Dispose(disposing);
        }
    }
}
