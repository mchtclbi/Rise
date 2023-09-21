using Space.Data.Concretes;
using Space.UserAPI.Data.Interfaces;
using Space.UserAPI.Models.Entities;

namespace Space.UserAPI.Data.Concretes
{
    public class UserRepository : EntityFrameworkRepository<User, UserContext>, IUserRepository
    {
    }
}