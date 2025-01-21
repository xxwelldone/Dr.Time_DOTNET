using DoctorTime.API.Entities;
using Microsoft.AspNetCore.Identity;

namespace DoctorTime.API.Security
{
    public class DoctorLogin : IdentityUser
    {
        public Doctor _doctor { get; set; }
    }
}
