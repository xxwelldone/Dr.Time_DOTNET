using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using DoctorTime.API.Entities;
using DoctorTime.API.Repository.Interfaces;
using DoctorTime.API.Security.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace DoctorTime.API.Security
{
    public class AuthenticateService : IAuthenticateService
    {

        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;

        public AuthenticateService(IConfiguration configuration, IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AuthenticateAsync(string email, string password)
        {
            User user = await _unitOfWork.UserRepository.GetByExpression(x => x.Email == email);
            Doctor doctor = await _unitOfWork.DoctorRepository.GetByExpression(x => x.Email == email);
            Worker worker = await _unitOfWork.WorkerRepository.GetByExpression(x => x.Email == email);
            if (user == null && doctor == null && worker == null)
            {
                return false;
            }
            else
            {
                if (user != null) { return VerifyHash(user, email, password); }
                else if (doctor != null) { return VerifyHash(doctor, email, password); }
                else
                {
                    return VerifyHash(worker, email, password);
                }

            }
        }
        public bool VerifyHash(AuthenticationUser user, string email, string password)
        {
            using HMACSHA512 hmac = new HMACSHA512(user.PasswordSalt);
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            for (int i = 0; i < hash.Length; i++)
            {
                if (hash[i] != user.PasswordHash[i])
                {
                    return false;
                }
            }
            return true;
        }
        public async Task<string> GenerateToken(string email)
        {
            User user = await _unitOfWork.UserRepository.GetByExpression(x => x.Email == email);
            Doctor doctor = await _unitOfWork.DoctorRepository.GetByExpression(x => x.Email == email);
            Worker worker = await _unitOfWork.WorkerRepository.GetByExpression(x => x.Email == email);

            if (user == null && doctor == null && worker == null)
            {
                throw new Exception("Usuário não encontrado");
            }
            else
            {
                if (user != null)
                {

                    return Token(user);

                }
                else if (doctor != null) { return Token(doctor); }
                else
                {
                    return Token(worker);
                }

            }

        }
        public string Token(User user)
        {

            Claim[] claims = new Claim[]
                {
                new Claim("id", user.Id.ToString()),
                new Claim("email", user.Email),
                new Claim("cpf", user.Cpf),
                new Claim("role", user.Role.ToString()),

                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

                };
            var privateKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwt:secretkey"]));
            var token = new JwtSecurityToken(
                expires: DateTime.Now.AddMinutes(30),
                claims: claims,
                signingCredentials: new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256),
                issuer: _configuration["jwt:issuer"],
                audience: _configuration["jwt:audience"]
                );
            return new JwtSecurityTokenHandler().WriteToken(token);

        }
        public string Token(Doctor doctor)
        {

            Claim[] claims = new Claim[]
                {
                new Claim("id", doctor.Id.ToString()),
                new Claim("email", doctor.Email),
               new Claim("crm", doctor.Crm),
               new Claim("speciality", doctor.Speciality.ToString()),
                new Claim("role", doctor.Role.ToString()),

                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

                };
            var privateKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwt:secretkey"]));
            var token = new JwtSecurityToken(
                expires: DateTime.Now.AddMinutes(30),
                claims: claims,
                signingCredentials: new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256),
                issuer: _configuration["jwt:issuer"],
                audience: _configuration["jwt:audience"]
                );
            return new JwtSecurityTokenHandler().WriteToken(token);

        }
        public string Token(Worker worker)
        {

            Claim[] claims = new Claim[]
                {
                new Claim("id", worker.Id.ToString()),
                new Claim("email", worker.Email),
               new Claim("crm", worker.Setor),
               new Claim("role", worker.Role.ToString()),



                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

                };
            var privateKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwt:secretkey"]));
            var token = new JwtSecurityToken(
                expires: DateTime.Now.AddMinutes(30),
                claims: claims,
                signingCredentials: new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256),
                issuer: _configuration["jwt:issuer"],
                audience: _configuration["jwt:audience"]
                );
            return new JwtSecurityTokenHandler().WriteToken(token);

        }


    }
}
