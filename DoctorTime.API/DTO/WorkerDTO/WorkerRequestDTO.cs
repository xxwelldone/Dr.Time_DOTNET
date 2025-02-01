using System.ComponentModel.DataAnnotations;

namespace DoctorTime.API.DTO.WorkerDTO
{
    public class WorkerRequestDTO
    {
        [Required(ErrorMessage ="Informe nome do usuário")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Informe setor do usuário")]

        public string Setor { get; set; }
        [Required(ErrorMessage = "Informe email do usuário")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Informe senha do usuário")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
