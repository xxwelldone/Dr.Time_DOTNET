using System.ComponentModel.DataAnnotations;

namespace DoctorTime.API.DTO.DoctorDTO
{
    public class DoctorUpdateDTO
    {
        public string? Name { get; set; }
      public string? PhtoUrl { get; set; }
        public string? Address { get; set; }
        [EmailAddress(ErrorMessage = "Informe e-mail válido")]

        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
