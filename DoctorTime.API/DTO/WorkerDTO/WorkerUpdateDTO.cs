using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DoctorTime.API.DTO.WorkerDTO
{
    public class WorkerUpdateDTO
    {
       public string? Name { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public string? Setor { get; set; }
        [DataType(DataType.Password)]
        public string? Password { get; set; }

    }
}
