using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DoctorTime.API.DTO.DoctorDTO;
using DoctorTime.API.Entities.Enums;

namespace DoctorTime.API.Entities
{
    public class Doctor :BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]

        public string PhtoUrl { get; set; }
        [Required]

        public string Crm {  get; set; }
        [Required]

        public string Address { get; set; }
        [EmailAddress(ErrorMessage ="Informe e-mail válido")]
        [Required]

        public string Email { get; set; }
        [Required]

        public string Password { get; set; }
        [Column(TypeName = "text")]
        public Role Role { get; private set; } = Role.DOCTOR;
        [Column(TypeName = "text")]
        [Required]

        public Speciality Speciality { get; set; }
        ICollection<Appointment> Appointments { get; set; }


        public Doctor(long id, string name, string phtoUrl, string address, string crm, string email, string password, Speciality speciality) :base(id)
        {
            Name = name;
            PhtoUrl = phtoUrl;
            Address = address;
            Email = email;
            Password = password;       
            Speciality = speciality;
            Crm = crm;
            Appointments = new Collection<Appointment>();
        }

        public Doctor()
        {
        }
        public void Update(DoctorUpdateDTO doctorUpdateDTO) {
            if (doctorUpdateDTO.Name != null)
            {
                Name = doctorUpdateDTO.Name;
            }
          
            if (doctorUpdateDTO.Email != null && doctorUpdateDTO.Email.Contains('@'))
            {
                Email= doctorUpdateDTO.Email;
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
