using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.ViewModels
{
    public class DoctorAddRequestViewModel
    {
        [Required]
        public string Name { get; set; }
        public string Surname { get; set; }
        public int DepartmentId { get; set; }
    }
}
