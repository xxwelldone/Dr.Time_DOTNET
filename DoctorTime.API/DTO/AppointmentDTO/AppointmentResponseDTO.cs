using DoctorTime.API.DTO.DoctorDTO;
using DoctorTime.API.DTO.UserDTO;
using DoctorTime.API.Entities.Enums;

namespace DoctorTime.API.DTO.AppointmentDTO
{
    public class AppointmentResponseDTO
    {
        public long Id { get; set; }
        public UserResponseDTO User { get; set; }
        public DoctorResposeDTO Doctor { get; set; }
        public DateOnly Date { get; set; }
       
        public TimeOnly Time { get; set; }
        public string Modality { get; set; }
        public Status Status { get; set; }
    }
}
