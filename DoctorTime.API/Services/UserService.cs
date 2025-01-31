using System.Linq.Expressions;
using AutoMapper;
using DoctorTime.API.DTO.LoginDTO;
using DoctorTime.API.DTO.UserDTO;
using DoctorTime.API.Entities;
using DoctorTime.API.Repository.Interfaces;
using DoctorTime.API.Security.Interfaces;
using DoctorTime.API.Services.Interfaces;

namespace DoctorTime.API.Services
{
    public class UserService : IUserService
    {
        private IUnitOfWork _unitOfWork;
        private ILoginService _loginService;
        private IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, ILoginService loginService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _loginService = loginService;
            _mapper = mapper;
        }

        public async Task<UserResponseDTO> Create(UserRequestDTO entity)
        {
            try
            {
                AuthenticationUser securedUser = await _loginService.CreateHashAsync(entity.Email, entity.Password);
                User user = _mapper.Map<User>(entity);
                user.PasswordHash = securedUser.PasswordHash;
                user.PasswordSalt = securedUser.PasswordSalt;
                await _unitOfWork.UserRepository.Create(user);
                await _unitOfWork.Commit();

                UserResponseDTO userRequestDTO = _mapper.Map<UserResponseDTO>(user);
                return userRequestDTO;
            }
            catch (Exception ex)
            {
                throw new Exception("Houve um erro ao salvar o usuário");
            }

        }

        public async Task<UserResponseDTO> Delete(long id)
        {
            User user = await _unitOfWork.UserRepository.GetByExpression(x => x.Id == id);
            _unitOfWork.UserRepository.Delete(user);
            await _unitOfWork.Commit();
            UserResponseDTO deleted = _mapper.Map<UserResponseDTO>(user);
            return deleted;
        }

        public async Task<IEnumerable<UserResponseDTO>> GetAllAsync()
        {
            IEnumerable<User> users = await _unitOfWork.UserRepository.GetAllAsync();
            IEnumerable<UserResponseDTO> userDTO = _mapper.Map<IEnumerable<UserResponseDTO>>(users);
            return userDTO;
        }

        public async Task<UserResponseDTO?> GetByIdAsync(Expression<Func<User, bool>> expression)
        {
            User user = await _unitOfWork.UserRepository.GetByExpression(expression);
            UserResponseDTO userResponseDTO = _mapper.Map<UserResponseDTO>(user);
            return userResponseDTO;
        }

        public async Task<UserResponseDTO> Update(long id, UserUpdateDTO userUpdateDTO)
        {
            User user = await _unitOfWork.UserRepository.GetByExpression(x => x.Id == id);
            user.Update(userUpdateDTO);
            _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.Commit();
            UserResponseDTO userResponseDTO = _mapper.Map<UserResponseDTO>(user);
            return userResponseDTO;
        }
    }
}
