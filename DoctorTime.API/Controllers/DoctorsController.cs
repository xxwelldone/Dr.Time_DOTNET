

using System.Collections.Generic;
using System.Net;
using AutoMapper;
using DoctorTime.API.Context;
using DoctorTime.API.DTO.DoctorDTO;
using DoctorTime.API.Entities;
using DoctorTime.API.Repository.Interfaces;
using DoctorTime.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DoctorTime.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DoctorsController : ControllerBase
    {
        IDoctorService _service;

        public DoctorsController(IDoctorService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize(Roles ="WORKER")]

        public async Task<ActionResult<IEnumerable<DoctorResposeDTO>>> Get()
        {
            try
            {
                IEnumerable<DoctorResposeDTO> doctorResposeDTOs = await _service.GetAllAsync();

                return Ok(doctorResposeDTOs);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }
        [HttpGet("{id}", Name = "GetDoctorById")]
       

        public async Task<ActionResult<DoctorResposeDTO>> GetById(long id)
        {
            
            try
            {
                DoctorResposeDTO doctorResposeDTO = await _service.GetById(id);

                return Ok(doctorResposeDTO);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("/{specialty}")]

        public async Task<ActionResult<IEnumerable<DoctorResposeDTO>>> GetBySpecialty(string specialty)
        {
            try
            {
                IEnumerable<DoctorResposeDTO> doctorResposeDTOs = await _service.GetBySpecialty(specialty);
                return Ok(doctorResposeDTOs);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPost]
        [Authorize(Roles ="WORKER")]
        public async Task<ActionResult<DoctorResposeDTO>> Post([FromBody] DoctorRequestDTO doctor)
        {

            try
            {
                DoctorResposeDTO doctorResposeDTO = await _service.Post(doctor);

                return new CreatedAtRouteResult(
                    routeName: "GetDoctorById",
                    routeValues: new { id = doctorResposeDTO.Id },
                    value: doctorResposeDTO
                    );
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        [Authorize(Roles ="DOCTOR")]

        public async Task<ActionResult<Doctor>> Put([FromBody] DoctorUpdateDTO doctorUpdateDto, long id)
        {

            try
            {
                DoctorResposeDTO doctorResposeDTO = await _service.Put(doctorUpdateDto, id);

                return Ok(doctorResposeDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpDelete("{id}")]
        [Authorize(Roles ="WORKER")]

        public async Task<ActionResult<DoctorResposeDTO>> Delete(long id)
        {

            try
            {
                DoctorResposeDTO deleted = await _service.Delete(id);
                return Ok(deleted);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

    }
}
