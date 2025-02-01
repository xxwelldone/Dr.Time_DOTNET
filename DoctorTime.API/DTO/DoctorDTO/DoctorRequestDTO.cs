using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using DoctorTime.API.Entities.Enums;

namespace DoctorTime.API.DTO.DoctorDTO
{
    public class DoctorRequestDTO
    {
        [Required(ErrorMessage = "Informe nome do usuário")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Informe URL para foto do usuário")]

        public string PhtoUrl { get; set; }
        [Required(ErrorMessage = "Informe CR do usuário, ele não deve ter sido cadastrado antes")]

        public string Crm { get; set; }
        [Required(ErrorMessage = "Informe endereço do usuário")]

        public string Address { get; set; }
        [EmailAddress(ErrorMessage = "Informe e-mail válido")]
        [Required]

        public string Email { get; set; }
        [Required(ErrorMessage = "Informe senha do usuário")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
     
        [Required(ErrorMessage = "Informe especialidade médica do usuário")]

        public Speciality Speciality { get; set; }
    }
}
