using DoctorTime.API.Entities.Enums;

namespace DoctorTime.API.DTO.WorkerDTO
{
    public class WorkerResponseDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Setor { get; set; }
        public Role Role { get; set; }
    }
}
