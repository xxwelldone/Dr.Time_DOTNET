using DoctorTime.API.DTO.AppointmentDTO;
using DoctorTime.API.DTO.WorkerDTO;

namespace DoctorTime.API.Services.Interfaces
{
    public interface IAppointmentService
    {
        Task<IEnumerable<AppointmentResponseDTO>> GetAllAsync();
        Task<AppointmentResponseDTO> GetByIdAsync(long id);
        Task<AppointmentResponseDTO> PostAsync(AppointmentRequestDTO request, long userId);
        Task<AppointmentResponseDTO> PutAsync(long id, AppointmentUpdateDTO updateDTO);
        Task<AppointmentResponseDTO> DeleteAsync(long id);
        Task<IEnumerable<AppointmentResponseDTO>> GetUserAppointmentsAsync(string email);
        Task<IEnumerable<AppointmentResponseDTO>> GetDoctorAppointmentsAsync(string email);


    }
}
