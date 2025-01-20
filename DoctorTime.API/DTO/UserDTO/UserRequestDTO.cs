using System.ComponentModel.DataAnnotations;

namespace DoctorTime.API.DTO.UserDTO
{
    public class UserRequestDTO
    {
        [Required]
        [MinLength(3, ErrorMessage = "Informe uma quantidade acima de 3 caracteres")]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        public string Cpf { get; set; }
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }


    }
}
