namespace HospitalManagement.Database.DomainModel
{
    public class Doctor
    {
        public Doctor()
            : this(default, default, default)
        {
          
        }

        public Doctor(string name, string surname, int departmentId)
        {
            Name = name;
            Surname = surname;
            DepartmentId = departmentId;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int? DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
