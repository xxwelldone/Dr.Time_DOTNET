using DoctorTime.API.Entities.Enums;

namespace DoctorTime.API.DTO.LoginDTO
{
    public class AuthenticationResponseDTO
    {
        public string Email { get; set; }
        public string Token { get; set; }
        
    }
}
