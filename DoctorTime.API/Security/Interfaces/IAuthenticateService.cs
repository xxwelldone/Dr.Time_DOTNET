

using DoctorTime.API.Entities;
using DoctorTime.API.Entities.Enums;

namespace DoctorTime.API.Security.Interfaces
{
    public interface IAuthenticateService
    {
        Task<bool> AuthenticateAsync(string email, string password);
        bool VerifyHash(AuthenticationUser user, string email, string password);
        Task<string> GenerateToken(string email);
        string Token(User user);
        string Token(Doctor doctor);
        string Token(Worker worker);
    }
}
