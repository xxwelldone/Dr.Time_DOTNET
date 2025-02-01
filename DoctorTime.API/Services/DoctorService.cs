using AutoMapper;
using DoctorTime.API.DTO.DoctorDTO;
using DoctorTime.API.Entities;
using DoctorTime.API.Repository.Interfaces;
using DoctorTime.API.Security.Interfaces;
using DoctorTime.API.Services.Interfaces;

namespace DoctorTime.API.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILoginService _loginService;

        public DoctorService(IUnitOfWork unitOfWork, IMapper mapper, ILoginService loginService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _loginService = loginService;
        }

        public async Task<DoctorResposeDTO> Delete(long id)
        {
            Doctor doctor = await _unitOfWork.DoctorRepository.GetByExpression(x => x.Id == id);
            _unitOfWork.DoctorRepository.Delete(doctor);
            await _unitOfWork.Commit();
            DoctorResposeDTO deleted = _mapper.Map<DoctorResposeDTO>(doctor);
            return deleted;
        }

        public async Task<IEnumerable<DoctorResposeDTO>> GetAllAsync()
        {
            IEnumerable<Doctor> doctors = await this._unitOfWork.DoctorRepository.GetAllAsync();

            IEnumerable<DoctorResposeDTO> doctorResposeDTOs = _mapper.Map<IEnumerable<DoctorResposeDTO>>(doctors);

            return doctorResposeDTOs;
        }

        public async Task<DoctorResposeDTO> GetById(long id)
        {
            Doctor doctor = await this._unitOfWork.DoctorRepository.GetByExpression(x => x.Id == id);
            DoctorResposeDTO doctorResposeDTO = _mapper.Map<DoctorResposeDTO>(doctor);
            return doctorResposeDTO;
        }

        public async Task<IEnumerable<DoctorResposeDTO>> GetBySpecialty(string specialty)
        {
            IEnumerable<Doctor> doctors = await _unitOfWork.DoctorRepository.GetBySpecialty(specialty);
            IEnumerable<DoctorResposeDTO> doctorResposeDTOs = _mapper.Map<IEnumerable<DoctorResposeDTO>>(doctors);
            return doctorResposeDTOs;
        }

        public async Task<DoctorResposeDTO> Post(DoctorRequestDTO doctor)
        {
            Doctor tobeSaved = _mapper.Map<Doctor>(doctor);
           
            AuthenticationUser authenticationUser = await _loginService.CreateHashAsync(doctor.Email, doctor.Password);
            tobeSaved.PasswordHash = authenticationUser.PasswordHash;
            tobeSaved.PasswordSalt = authenticationUser.PasswordSalt;
            Doctor savedDoctor = await this._unitOfWork.DoctorRepository.Create(tobeSaved);
            await _unitOfWork.Commit();
            DoctorResposeDTO doctorResposeDTO = _mapper.Map<DoctorResposeDTO>(savedDoctor);
            return doctorResposeDTO;
        }

        public async Task<DoctorResposeDTO> Put(DoctorUpdateDTO doctorUpdateDto, long id)
        {
            Doctor doctor = await _unitOfWork.DoctorRepository.GetByExpression(x => x.Id == id);

            doctor.Update(doctorUpdateDto);

            _unitOfWork.DoctorRepository.Update(doctor);
            await _unitOfWork.Commit();
            DoctorResposeDTO doctorResposeDTO = _mapper.Map<DoctorResposeDTO>(doctor);
            return doctorResposeDTO;

        }
    }
}
