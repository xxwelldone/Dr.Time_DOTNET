using System.ComponentModel.DataAnnotations;

namespace DoctorTime.API.DTO.UserDTO
{
    public class UserUpdateDTO
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
    }
}
