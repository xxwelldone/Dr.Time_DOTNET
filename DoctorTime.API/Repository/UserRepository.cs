using DoctorTime.API.Context;
using DoctorTime.API.Entities;
using DoctorTime.API.Repository.Interfaces;

namespace DoctorTime.API.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(PostgreSQL context) : base(context)
        {
        }
    }
}
