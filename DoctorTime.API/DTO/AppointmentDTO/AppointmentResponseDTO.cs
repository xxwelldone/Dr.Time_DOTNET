using DoctorTime.API.DTO.DoctorDTO;
using DoctorTime.API.DTO.UserDTO;
using DoctorTime.API.Entities.Enums;

namespace DoctorTime.API.DTO.AppointmentDTO
{
    public class AppointmentResponseDTO
    {
        public long Id;
        public UserResponseDTO User;
        public DoctorResposeDTO Doctor;
        public DateTime DateTime;
        public string Modality;
        public Status Status;
    }
}
