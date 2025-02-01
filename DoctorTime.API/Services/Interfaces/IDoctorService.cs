using AutoMapper;
using DoctorTime.API.DTO.DoctorDTO;
using DoctorTime.API.Entities;
using DoctorTime.API.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DoctorTime.API.Services.Interfaces
{
    public interface IDoctorService
    {

        Task<IEnumerable<DoctorResposeDTO>> GetAllAsync();
        Task<DoctorResposeDTO> GetById(long id);
        Task<IEnumerable<DoctorResposeDTO>> GetBySpecialty(string specialty);
        Task<DoctorResposeDTO> Post(DoctorRequestDTO doctor);
        Task<DoctorResposeDTO> Put(DoctorUpdateDTO doctorUpdateDto, long id);
        Task<DoctorResposeDTO> Delete(long id);

    }

}
