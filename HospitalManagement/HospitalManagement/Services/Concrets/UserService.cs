using HospitalManagement.Database;
using HospitalManagement.Database.DomainModel;
using HospitalManagement.Services.Abstract;

namespace HospitalManagement.Services.Concrets
{
    public class UserService : IUserService
    {
        private readonly HospitalDbContext _hospitalDbContext;

        public UserService(HospitalDbContext hospitalDbContext)
        {
            _hospitalDbContext = hospitalDbContext;
        }

        public User GetCurrentLoggedUser()
        {
            return _hospitalDbContext.Users.Single(u => u.Id == -1);
        }
    }
}
