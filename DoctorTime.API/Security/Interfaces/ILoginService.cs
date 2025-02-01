using DoctorTime.API.DTO.LoginDTO;
using DoctorTime.API.Entities;

namespace DoctorTime.API.Security.Interfaces
{
    public interface ILoginService
    {
        Task<AuthenticationUser> CreateHashAsync(string email, string password);
        Task<AuthenticationResponseDTO> LoginAsync(string email, string password);
    }
}
