using DoctorTime.API.Entities;

namespace DoctorTime.API.Repository.Interfaces
{
    public interface IAppointmentRepository : IBaseRepository<Appointment>
    {
        Task<IEnumerable<Appointment>> GetAllMyUserAppoinments(string email);
        Task<IEnumerable<Appointment>> GetAllMyDoctorAppoinments(string email);
    }
}
