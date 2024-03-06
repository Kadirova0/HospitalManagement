using HospitalManagement.Database.DomainModel;

namespace HospitalManagement.ViewModels
{
    public class DoctorAddResponseViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int? DepartmentId { get; set; }
        public List<Department> Departments { get; set; }

        
    }
}
