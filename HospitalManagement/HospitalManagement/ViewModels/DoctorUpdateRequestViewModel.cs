﻿namespace HospitalManagement.ViewModels
{
    public class DoctorUpdateRequestViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int? DepartmentId { get; set; }
    }
}
