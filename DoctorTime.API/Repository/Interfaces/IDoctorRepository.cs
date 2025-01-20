using DoctorTime.API.Entities;

namespace DoctorTime.API.Repository.Interfaces
{
    public interface IDoctorRepository : IBaseRepository<Doctor>
    {
        Task<IEnumerable<Doctor>> GetBySpecialty(string specialty);

    }
}
