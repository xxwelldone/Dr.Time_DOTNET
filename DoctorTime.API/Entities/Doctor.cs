using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DoctorTime.API.DTO.DoctorDTO;
using DoctorTime.API.Entities.Enums;

namespace DoctorTime.API.Entities
{
    public class Doctor : BaseEntity, AuthenticationUser
    {
        public string Name { get; set; }

        public string PhtoUrl { get; set; }

        public string Crm { get; set; }

        public string Address { get; set; }


        public string Email { get; set; }


        public Role Role { get; private set; } = Role.DOCTOR;

        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }


        public Speciality Speciality { get; set; }
      public  ICollection<Appointment> Appointments { get; set; }


        public Doctor(long id, string name, string phtoUrl, string address, string crm, string email, byte[] passwordHash, byte[] passwordSalt, Speciality speciality) : base(id)
        {
            Name = name;
            PhtoUrl = phtoUrl;
            Address = address;
            Email = email;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
            Speciality = speciality;
            Crm = crm;
            Appointments = new Collection<Appointment>();
        }

        public Doctor()
        {
        }
        public void Update(DoctorUpdateDTO doctorUpdateDTO)
        {
            if (doctorUpdateDTO.Name != null)
            {
                Name = doctorUpdateDTO.Name;
            }

            if (doctorUpdateDTO.Email != null && doctorUpdateDTO.Email.Contains('@'))
            {
                Email = doctorUpdateDTO.Email;
            }
            if (doctorUpdateDTO.Address != null)
            {
                Address = doctorUpdateDTO.Address;
            }
            if (doctorUpdateDTO.PhtoUrl != null)
            {
                PhtoUrl = doctorUpdateDTO.PhtoUrl;
            }
        }
    }
}
