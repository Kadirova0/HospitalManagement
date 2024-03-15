using HospitalManagement.Contracts;
using HospitalManagement.Database.DomainModel;
using HospitalManagement.ViewModels;
using Npgsql;
using System;

namespace HospitalManagement.Database.Repositories;

public class DoctorRepository : IDisposable
{
    private readonly NpgsqlConnection _npgsqlConnection;

    public DoctorRepository()
    {
        _npgsqlConnection = new NpgsqlConnection(DatabaseConstants.CONNECTION_STRING);
        _npgsqlConnection.Open();
    }


    public List<Doctor> GetAll()
    {
        var selectQuery = "SELECT * FROM doctors ORDER BY name";

        using NpgsqlCommand command = new NpgsqlCommand(selectQuery, _npgsqlConnection);
        using NpgsqlDataReader dataReader = command.ExecuteReader();
        List<Doctor> doctors = new List<Doctor>();

        while (dataReader.Read())
        {
            Doctor doctor = new Doctor
            {
                Id = Convert.ToInt32(dataReader["id"]),
                Name = Convert.ToString(dataReader["name"]),
                Surname = Convert.ToString(dataReader["surname"]),
                DepartmentId = dataReader["departmentid"] as int?,
            };
            doctors.Add(doctor);
        }
        return doctors;
    }

    public List<Doctor> GetAllWithDepartments()
    {
        var selectQuery = "SELECT \r\n  t.\"id\" doctorId,\r\n  t.\"name\" doctorName,\r\n  t.\"surname\" doctorSurname,\r\n  d.\"id\" departmentId,\r\n  d.\"name\" departmentName\r\nFROM doctors t\r\nLEFT JOIN departments d ON t.\"departmentid\"=d.\"id\"\r\nORDER BY t.name";

        using NpgsqlCommand command = new NpgsqlCommand(selectQuery, _npgsqlConnection);
        using NpgsqlDataReader dataReader = command.ExecuteReader();
        List<Doctor> doctors = new List<Doctor>();

        while (dataReader.Read())
        {
            Doctor doctor = new Doctor
            {
                Id = Convert.ToInt32(dataReader["doctorId"]),
                Name = Convert.ToString(dataReader["doctorName"]),
                Surname = Convert.ToString(dataReader["doctorSurname"]),
                DepartmentId = dataReader["departmentId"] as int?,
                Department = dataReader["departmentId"] is int 
                           ? new Department(Convert.ToInt32(dataReader["departmentId"]), Convert.ToString(dataReader["departmentName"])) 
                           :null
            };
            doctors.Add(doctor);
        }
        return doctors;
    }

    public Doctor GetById(int id)
    {

        using NpgsqlCommand command = new NpgsqlCommand($"SELECT * FROM doctors WHERE id={id}", _npgsqlConnection);
        using NpgsqlDataReader dataReader = command.ExecuteReader();
        Doctor doctor = null;

        while (dataReader.Read())
        {
            doctor = new Doctor
            {
                Id = Convert.ToInt32(dataReader["id"]),
                Name = Convert.ToString(dataReader["name"]),
                Surname = Convert.ToString(dataReader["surname"]),
                DepartmentId = dataReader["departmentid"] as int?,
            };
        }
        return doctor;
    }


    public void Insert(Doctor doctor)
    {
        string updateQuery =
            $"INSERT INTO doctors(name, surname, departmentid)" +
            $"VALUES ('{doctor.Name}', '{doctor.Surname}', {doctor.DepartmentId})";
        using NpgsqlCommand command = new NpgsqlCommand(updateQuery, _npgsqlConnection);
        command.ExecuteNonQuery();
    }

    public void Update(Doctor doctor)
    {
        var query =
                    $"UPDATE doctors " +
                    $"SET name='{doctor.Name}', surname='{doctor.Surname}', departmentid={doctor.DepartmentId} " +
                    $"WHERE id={doctor.Id}";

        using NpgsqlCommand updateCommand = new NpgsqlCommand(query, _npgsqlConnection);
        updateCommand.ExecuteNonQuery();
    }

    public void RemoveById(int id)
    {
        var query =
           $"DELETE FROM doctors WHERE id={id}";
        using NpgsqlCommand updateCommand = new NpgsqlCommand(query, _npgsqlConnection);
        updateCommand.ExecuteNonQuery();
    }

    public void Dispose()
    {
        _npgsqlConnection.Dispose();
    }
}
