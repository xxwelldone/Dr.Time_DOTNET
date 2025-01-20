using DoctorTime.API.Context;
using DoctorTime.API.Entities;
using DoctorTime.API.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DoctorTime.API.Repository
{
    public class DoctorRepository : BaseRepository<Doctor>, IDoctorRepository
    {
        public DoctorRepository(PostgreSQL context) : base(context)
        {
        }

        public async Task<IEnumerable<Doctor>> GetBySpecialty(string specialty)
        {
            return await _context.Doctors.Where(x => x.Speciality.ToString().ToUpper() == specialty.ToUpper()).ToListAsync();
        }
    }
}
