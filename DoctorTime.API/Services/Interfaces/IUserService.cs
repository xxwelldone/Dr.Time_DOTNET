using System.Linq.Expressions;
using DoctorTime.API.DTO.UserDTO;
using DoctorTime.API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DoctorTime.API.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserResponseDTO>> GetAllAsync();
        Task<UserResponseDTO?> GetByIdAsync(Expression<Func<User, bool>> expression);

        Task<UserResponseDTO> Create(UserRequestDTO entity);
        Task<UserResponseDTO?> Update(long id, UserUpdateDTO userUpdateDTO);
        Task<UserResponseDTO?> Delete(long id);
    }
}
