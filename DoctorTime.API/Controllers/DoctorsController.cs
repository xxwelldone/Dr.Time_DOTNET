

using System.Net;
using AutoMapper;
using DoctorTime.API.Context;
using DoctorTime.API.DTO.DoctorDTO;
using DoctorTime.API.Entities;
using DoctorTime.API.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DoctorTime.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DoctorsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DoctorResposeDTO>>> Get()
        {
            IEnumerable<Doctor> doctors = await this._unitOfWork.DoctorRepository.GetAllAsync();

            IEnumerable<DoctorResposeDTO> doctorResposeDTOs = _mapper.Map<IEnumerable<DoctorResposeDTO>>(doctors);

            return Ok(doctorResposeDTOs);
        }
        [HttpGet("{id}", Name = "GetDoctorById")]
        public async Task<ActionResult<DoctorResposeDTO>> GetById(long id)
        {
            Doctor doctor = await this._unitOfWork.DoctorRepository.GetByExpression(x => x.Id == id);
            DoctorResposeDTO doctorResposeDTO = _mapper.Map<DoctorResposeDTO>(doctor);

            return Ok(doctorResposeDTO);
        }
        [HttpGet("/{specialty}")]
        public async Task<ActionResult<IEnumerable<DoctorResposeDTO>>> GetBySpecialty(string specialty)
        {
            IEnumerable<Doctor> doctors = await _unitOfWork.DoctorRepository.GetBySpecialty(specialty);
            IEnumerable<DoctorResposeDTO> doctorResposeDTOs = _mapper.Map<IEnumerable<DoctorResposeDTO>>(doctors);
            return Ok(doctorResposeDTOs);
        }
        [HttpPost]
        public async Task<ActionResult<DoctorResposeDTO>> Post([FromBody] DoctorRequestDTO doctor)
        {
            Doctor tobeSaved = _mapper.Map<Doctor>(doctor);
            Doctor savedDoctor = await this._unitOfWork.DoctorRepository.Create(tobeSaved);
            await _unitOfWork.Commit();
            DoctorResposeDTO doctorResposeDTO = _mapper.Map<DoctorResposeDTO>(savedDoctor);

            return new CreatedAtRouteResult(
                routeName: "GetDoctorById",
                routeValues: new { id = doctorResposeDTO.Id },
                value: doctorResposeDTO
                );
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Doctor>> Put([FromBody] DoctorUpdateDTO doctorUpdateDto, long id)
        {
            Doctor doctor = await _unitOfWork.DoctorRepository.GetByExpression(x => x.Id == id);

            doctor.Update(doctorUpdateDto);

            _unitOfWork.DoctorRepository.Update(doctor);
            await _unitOfWork.Commit();
            DoctorResposeDTO doctorResposeDTO = _mapper.Map<DoctorResposeDTO>(doctor);

            return Ok(doctorResposeDTO);

        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<DoctorResposeDTO>> Delete(long id)
        {
            Doctor doctor = await _unitOfWork.DoctorRepository.GetByExpression(x => x.Id == id);
            _unitOfWork.DoctorRepository.Delete(doctor);
            await _unitOfWork.Commit();
            DoctorResposeDTO deleted = _mapper.Map<DoctorResposeDTO>(doctor);
            return Ok(deleted);
        }

    }
}
