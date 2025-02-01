using DoctorTime.API.DTO.WorkerDTO;
using DoctorTime.API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DoctorTime.API.Services.Interfaces
{
    public interface IWorkerService
    {

        Task<IEnumerable<WorkerResponseDTO>> GetAsync();
        Task<WorkerResponseDTO> GetByIdAsync(long id);
        Task<WorkerResponseDTO> PostAsync(WorkerRequestDTO worker);
        Task<WorkerResponseDTO> PutAsync(long id, WorkerUpdateDTO workerUpdateDTO);
        Task<WorkerResponseDTO> DeleteAsync(long id);



    }
}
