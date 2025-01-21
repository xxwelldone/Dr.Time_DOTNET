using DoctorTime.API.Entities;
using Microsoft.AspNetCore.Identity;

namespace DoctorTime.API.Security
{
    public class UserLogin : IdentityUser
    {
        private User _user { get; set; }
    }
}
