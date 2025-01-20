using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DoctorTime.API.DTO.DoctorDTO;
using System.Net;
using DoctorTime.API.Entities.Enums;
using DoctorTime.API.DTO.UserDTO;

namespace DoctorTime.API.Entities
{
    public class User : BaseEntity
    {
        [Required]

        public string Name { get; set; }
        [EmailAddress(ErrorMessage ="Informe um e-mail válido")]
        public string Email { get; set; }
        [Required]
        public string Address;
        [Required]

        public string Cpf { get; set; }
        [Required]

        public string Password { get; set; }
        [Column(TypeName = "text")]
        public Role Role { get; private set; } = Role.USER;
        ICollection<Appointment> Appointments { get; set; }

        public User(long id, string name,string address, string email, string cpf, string password) : base(id)
        {
            Name = name;
            Address = address;
            Email = email;
            Cpf = cpf;
            Password = password;
            Appointments = new Collection<Appointment>();

        }

        public User()
        {
        }
        public void Update(UserUpdateDTO UserUpdateDTO)
        {
            if (UserUpdateDTO.Name != null)
            {
                Name = UserUpdateDTO.Name;
            }

        
            if (UserUpdateDTO.Address != null)
            {
                Address = UserUpdateDTO.Address;
            }
            
        }
    }
}
