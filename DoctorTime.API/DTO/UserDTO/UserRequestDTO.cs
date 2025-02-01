using System.ComponentModel.DataAnnotations;

namespace DoctorTime.API.DTO.UserDTO
{
    public class UserRequestDTO
    {
        [Required]
        [MinLength(3, ErrorMessage = "Informe uma quantidade acima de 3 caracteres")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Informe endereço do usuário")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Informe CPF do usuário")]
        public string Cpf { get; set; }
        [Required(ErrorMessage = "Informe emal do usuário")]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Informe senha do usuário")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


    }
}
