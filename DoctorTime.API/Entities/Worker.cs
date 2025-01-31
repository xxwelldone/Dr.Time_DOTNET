using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DoctorTime.API.Entities.Enums;
using Microsoft.AspNetCore.Identity;

namespace DoctorTime.API.Entities
{
    public class Worker : BaseEntity, AuthenticationUser
    {

        public string Name { get; set; }
        public string Email { get; set; }

        public string Setor { get; set; }
   
        public Role Role { get; private set; } = Role.WORKER;

        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public Worker(long id, string name, string email, string setor, byte[] passwordHash, byte[] passwordSalt) : base(id)
        {
            Name = name;
            Email = email;
            Setor = setor;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;


        }

        public Worker()
        {
        }
    }
}
