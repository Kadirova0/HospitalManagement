using HospitalManagement.Database.DomainModel;

namespace HospitalManagement.ViewModels;

public class DoctorUpdateResponseViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public int? DepartmentId { get; set; }
    public List<Department> Departments { get; set; }

}
