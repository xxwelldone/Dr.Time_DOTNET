using AutoMapper;
using DoctorTime.API.DTO.WorkerDTO;
using DoctorTime.API.Entities;
using DoctorTime.API.Repository.Interfaces;
using DoctorTime.API.Security.Interfaces;
using DoctorTime.API.Services.Interfaces;

namespace DoctorTime.API.Services
{
    public class WorkerService : IWorkerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILoginService _loginService;

        public WorkerService(IUnitOfWork unitOfWork, IMapper mapper, ILoginService loginService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _loginService = loginService;
        }

        public async Task<WorkerResponseDTO> DeleteAsync(long id)
        {
            Worker worker = await _unitOfWork.WorkerRepository.GetByExpression(x => x.Id == id);
            if (worker is null)
            {
                throw new Exception("Not found");
            }
            _unitOfWork.WorkerRepository.Delete(worker);
            await _unitOfWork.Commit();
            WorkerResponseDTO workerResponseDTO = _mapper.Map<WorkerResponseDTO>(worker);
            return workerResponseDTO;
        }

        public async Task<IEnumerable<WorkerResponseDTO>> GetAsync()
        {
            IEnumerable<Worker> workers = await _unitOfWork.WorkerRepository.GetAllAsync();
            IEnumerable<WorkerResponseDTO> workerResponseDTOs = _mapper.Map<IEnumerable<WorkerResponseDTO>>(workers);
            return workerResponseDTOs;

        }

        public async Task<WorkerResponseDTO> GetByIdAsync(long id)
        {
            Worker worker = await _unitOfWork.WorkerRepository.GetByExpression(x => x.Id == id);
            WorkerResponseDTO workerResponseDTO = _mapper.Map<WorkerResponseDTO>(worker);
            return workerResponseDTO;
        }

        public async Task<WorkerResponseDTO> PostAsync(WorkerRequestDTO workerRequestDTO)
        {
            Worker worker = _mapper.Map<Worker>(workerRequestDTO);
            var authenticatedUser = await _loginService.CreateHashAsync(workerRequestDTO.Email, workerRequestDTO.Password);
            worker.PasswordHash = authenticatedUser.PasswordHash;
            worker.PasswordSalt = authenticatedUser.PasswordSalt;
            await _unitOfWork.WorkerRepository.Create(worker);
            await _unitOfWork.Commit();
            WorkerResponseDTO workerResponse = _mapper.Map<WorkerResponseDTO>(worker);
            return workerResponse;
        }

        public async Task<WorkerResponseDTO> PutAsync(long id, WorkerUpdateDTO workerUpdateDTO)
        {
            Worker worker = await _unitOfWork.WorkerRepository.GetByExpression(x => x.Id == id);
            if (worker is null)
            {
                throw new Exception("Usuário não encontrado");
            }
            worker.Update(workerUpdateDTO, null, null);


            _unitOfWork.WorkerRepository.Update(worker);
            await _unitOfWork.Commit();
            WorkerResponseDTO workerResponseDTO = _mapper.Map<WorkerResponseDTO>(worker);
            return workerResponseDTO;

        }
    }
}
