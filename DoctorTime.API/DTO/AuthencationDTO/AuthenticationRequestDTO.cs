using System.ComponentModel.DataAnnotations;

namespace DoctorTime.API.DTO.LoginDTO
{
    public class AuthenticationRequestDTO
    {
        [Required(ErrorMessage = "Informe emal do usuário")]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        public string Email {get;set;}
        [Required(ErrorMessage = "Informe senha do usuário")]
        [DataType(DataType.Password)]
        public string Password {get;set;}
    }
}
