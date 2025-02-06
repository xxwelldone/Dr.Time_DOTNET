using System.ComponentModel.DataAnnotations;

namespace DoctorTime.API.DTO.AppointmentDTO
{
    public class AppointmentRequestDTO
    {
        [Required]
        public long DoctorId { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
        [Required]
        public string Modality { get; set; }
    }
}
