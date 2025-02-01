using System.Security.Cryptography;
using System.Text;
using DoctorTime.API.DTO.AuthencationDTO;
using DoctorTime.API.DTO.LoginDTO;
using DoctorTime.API.Entities;
using DoctorTime.API.Repository.Interfaces;
using DoctorTime.API.Security.Interfaces;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace DoctorTime.API.Security
{
    public class LoginService : ILoginService
    {

        IUnitOfWork _unitOfWork;
        IAuthenticateService _authenticateService;

        public LoginService(IUnitOfWork unitOfWork, IAuthenticateService authenticateService)
        {
            _unitOfWork = unitOfWork;
            _authenticateService = authenticateService;
        }


        public async Task<AuthenticationUser> CreateHashAsync(string email, string password)
        {
            User user = await _unitOfWork.UserRepository.GetByExpression(x => x.Email == email);
            Doctor doctor = await _unitOfWork.DoctorRepository.GetByExpression(x => x.Email == email);
            Worker worker = await _unitOfWork.WorkerRepository.GetByExpression(x => x.Email == email);

            if (user != null || doctor != null || worker != null)
            {
                throw new Exception("Usuário já existe");
            }
            else
            {
                using HMAC crypt = new HMACSHA512();
                byte[] passwordHash = crypt.ComputeHash(Encoding.UTF8.GetBytes(password));
                byte[] passwordSalt = crypt.Key;

                return new AuthenticationUserDTO { Email = email, PasswordHash = passwordHash, PasswordSalt = passwordSalt };

            }

        }

        public async Task<AuthenticationResponseDTO> LoginAsync(string email, string password)
        {

            User user = await _unitOfWork.UserRepository.GetByExpression(x => x.Email.ToUpper() == email.ToUpper());
            Doctor doctor = await _unitOfWork.DoctorRepository.GetByExpression(x => x.Email.ToUpper() == email.ToUpper());
            Worker worker = await _unitOfWork.WorkerRepository.GetByExpression(x => x.Email.ToUpper() == email.ToUpper());

            if (user == null && doctor == null && worker == null)
            {
                throw new Exception("Usuário não existe");
            }
            else
            {
                bool isVerified = await _authenticateService.AuthenticateAsync(email, password);
                if (!isVerified)
                {
                    throw new Exception("Usuário ou senha incorretos");
                }

                string token = await _authenticateService.GenerateToken(email);
                return new AuthenticationResponseDTO { Email = email,Token = token };
            }


        }

    }
}
