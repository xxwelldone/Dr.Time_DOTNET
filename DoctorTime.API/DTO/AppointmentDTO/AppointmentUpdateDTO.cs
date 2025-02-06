using DoctorTime.API.Entities.Enums;

namespace DoctorTime.API.DTO.AppointmentDTO
{
    public class AppointmentUpdateDTO
    {
        public DateTime? DateTime { get; set; }
        public string? Modality { get; set; }
        public Status? Status { get; set; }
    }
}
