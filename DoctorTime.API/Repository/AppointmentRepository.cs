using DoctorTime.API.Context;
using DoctorTime.API.Entities;
using DoctorTime.API.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace DoctorTime.API.Repository
{
    public class AppointmentRepository : BaseRepository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(PostgreSQL context) : base(context)
        {
        }
        public async Task<IEnumerable<Appointment>> GetAllMyUserAppoinments(string email)
        {                        
            return await _context.Appointments.Where(x => x.User.Email == email).ToListAsync();
        }
        public async Task<IEnumerable<Appointment>> GetAllMyDoctorAppoinments(string email)
        {
            return await _context.Appointments.Where(x => x.Doctor.Email == email).ToListAsync();
        }
    }
}
