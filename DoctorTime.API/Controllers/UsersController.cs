using AutoMapper;
using DoctorTime.API.DTO.UserDTO;
using DoctorTime.API.Entities;
using DoctorTime.API.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DoctorTime.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UsersController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserResponseDTO>>> Get()
        {
            IEnumerable<User> users = await _unitOfWork.UserRepository.GetAllAsync();
            IEnumerable<UserResponseDTO> userDTO = _mapper.Map<IEnumerable<UserResponseDTO>>(users);
            return Ok(userDTO);
        }
        [HttpGet("{id}", Name = "GetUserById")]
        public async Task<ActionResult<UserResponseDTO>> GetById(long id)
        {
            User user = await _unitOfWork.UserRepository.GetByExpression(x => x.Id == id);
            UserResponseDTO userResponseDTO = _mapper.Map<UserResponseDTO>(user);
            return Ok(userResponseDTO);
        }
        [HttpPost]
        public async Task<ActionResult<UserResponseDTO>> Post([FromBody] UserRequestDTO userRequest)
        {
           User user = _mapper.Map<User>(userRequest);
            User createdUser = await _unitOfWork.UserRepository.Create(user);
            await _unitOfWork.Commit();
            UserResponseDTO userResponseDTO = _mapper.Map<UserResponseDTO>(createdUser);
            return new CreatedAtRouteResult(
                routeName: "GetUserById",
                routeValues: new { id = userResponseDTO.Id },
                value: userResponseDTO);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<UserResponseDTO>> Put(long id, [FromBody] UserUpdateDTO userUpdateDTO)
        {
            User user = await _unitOfWork.UserRepository.GetByExpression(x => x.Id == id);
            user.Update(userUpdateDTO);
            _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.Commit();
            UserResponseDTO userResponseDTO = _mapper.Map<UserResponseDTO>(user);
            return Ok(userResponseDTO);

        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserResponseDTO>> Delete(long id)
        {
            User user = await _unitOfWork.UserRepository.GetByExpression(x => x.Id == id);
            _unitOfWork.UserRepository.Delete(user);
            await _unitOfWork.Commit();
            UserResponseDTO deleted = _mapper.Map<UserResponseDTO>(user);
            return Ok(deleted);
        }
    }
}
