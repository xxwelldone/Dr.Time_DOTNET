using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DoctorTime.API.DTO.DoctorDTO;
using System.Net;
using DoctorTime.API.Entities.Enums;
using DoctorTime.API.DTO.UserDTO;

namespace DoctorTime.API.Entities
{
    public class User : BaseEntity, AuthenticationUser
    {

        public string Name { get; set; }
        public string Email { get; set; }
        public string Address;
        public string Cpf { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public Role Role { get; private set; } = Role.USER;
        public ICollection<Appointment> Appointments { get; set; }

        public User(long id, string name, string address, string email, string cpf, byte[] passwordHash, byte[] passwordSalt) : base(id)
        {
            Name = name;
            Address = address;
            Email = email;
            Cpf = cpf;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
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
