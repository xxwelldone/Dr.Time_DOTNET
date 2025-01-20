using System.Runtime.InteropServices;

namespace DoctorTime.API.DTO.UserDTO
{
    public class UserResponseDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
    }
}
