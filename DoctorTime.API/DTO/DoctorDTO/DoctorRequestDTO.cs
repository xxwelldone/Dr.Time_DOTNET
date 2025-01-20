using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using DoctorTime.API.Entities.Enums;

namespace DoctorTime.API.DTO.DoctorDTO
{
    public class DoctorRequestDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]

        public string PhtoUrl { get; set; }
        [Required]

        public string Crm { get; set; }
        [Required]

        public string Address { get; set; }
        [EmailAddress(ErrorMessage = "Informe e-mail válido")]
        [Required]

        public string Email { get; set; }
        [Required]

        public string Password { get; set; }
     
        [Required]

        public Speciality Speciality { get; set; }
    }
}
