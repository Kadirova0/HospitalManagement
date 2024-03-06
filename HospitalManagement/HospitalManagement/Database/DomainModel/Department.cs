namespace HospitalManagement.Database.DomainModel
{
    public class Department
    {
        public Department() 
        {
        }


        public Department(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
