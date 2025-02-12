﻿using System.Security.Claims;
using AutoMapper;
using DoctorTime.API.DTO.UserDTO;
using DoctorTime.API.Entities;
using DoctorTime.API.Repository.Interfaces;
using DoctorTime.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DoctorTime.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {

        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;

        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<UserResponseDTO>>> Get()
        {
            try
            {
                IEnumerable<UserResponseDTO> userResponseDTOs = await _userService.GetAllAsync();
                return Ok(userResponseDTOs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}", Name = "GetUserById")]


        public async Task<ActionResult<UserResponseDTO>> GetById(long id)
        {
            try
            {
                UserResponseDTO userResponseDTO = await _userService.GetByIdAsync(x => x.Id == id);
                return Ok(userResponseDTO);

            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPost]
        [AllowAnonymous]

        public async Task<ActionResult<UserResponseDTO>> Post([FromBody] UserRequestDTO userRequest)
        {
            try
            {
                UserResponseDTO userResponseDTO = await _userService.Create(userRequest);
                return new CreatedAtRouteResult(
                    routeName: "GetUserById",
                    routeValues: new { id = userResponseDTO.Id },
                    value: userResponseDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
  

        public async Task<ActionResult<UserResponseDTO>> Put(long id, [FromBody] UserUpdateDTO userUpdateDTO)
        {
            try
            {
                UserResponseDTO userResponseDTO = await _userService.Update(id, userUpdateDTO);
                return Ok(userResponseDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpDelete("{id}")]
        [Authorize(Roles ="WORKER")]

        public async Task<ActionResult<UserResponseDTO>> Delete(long id)
        {
            try
            {
                UserResponseDTO deleted = await _userService.Delete(id);
                return Ok(deleted);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
