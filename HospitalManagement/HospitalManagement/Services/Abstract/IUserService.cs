using HospitalManagement.Database.DomainModel;

namespace HospitalManagement.Services.Abstract
{
    public interface IUserService
    {
        User GetCurrentLoggedUser();
    }
}
