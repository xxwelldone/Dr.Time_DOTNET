using System.ComponentModel.DataAnnotations;

namespace DoctorTime.API.Security
{
    public class Login
    {
        [EmailAddress]
        [Required]
        public string Email;
        [Required]
        public string Password;
    }
}
