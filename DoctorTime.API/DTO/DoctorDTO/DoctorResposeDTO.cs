using DoctorTime.API.Entities.Enums;
using System.Runtime.InteropServices;

namespace DoctorTime.API.DTO.DoctorDTO
{
    public class DoctorResposeDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string PhtoUrl { get; set; }
        public string Crm { get; set; }
        public Speciality Specialty { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
    }
}
