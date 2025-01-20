using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DoctorTime.API.Entities.Enums;
using Microsoft.AspNetCore.Identity;

namespace DoctorTime.API.Entities
{
    public class Worker : BaseEntity
    {
        [Required]

        public string Name { get; set; }
        [EmailAddress(ErrorMessage ="Informe um e-mail válido")]
        public string Email { get; set; }
        [Required]

        public string Setor { get; set; }
        public string Password { get; set; }
        [Column(TypeName = "text")]
        public Role Role { get; private set; } = Role.WORKER;

        

        public Worker(long id, string name, string email, string setor, string password) : base(id)
        {
            Name = name;
            Email = email;
            Setor = setor;
            Password = password;
        }

        public Worker()
        {
        }
    }
}
