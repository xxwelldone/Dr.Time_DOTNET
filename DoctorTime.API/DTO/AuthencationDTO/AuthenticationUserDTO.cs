using DoctorTime.API.Entities;

namespace DoctorTime.API.DTO.AuthencationDTO
{
    public class AuthenticationUserDTO : AuthenticationUser
    {
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
