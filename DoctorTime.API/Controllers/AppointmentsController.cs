using DoctorTime.API.DTO.AppointmentDTO;
using DoctorTime.API.DTO.WorkerDTO;
using DoctorTime.API.Services;
using DoctorTime.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DoctorTime.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService _appointmentsService;

        public AppointmentsController(IAppointmentService appointmentsService)
        {
            _appointmentsService = appointmentsService;
        }
        [HttpGet]
        [Authorize(Roles ="WORKER")]
        public async Task<ActionResult<IEnumerable<AppointmentResponseDTO>>> GetAll()
        {
            try
            {
                return Ok(await _appointmentsService.GetAllAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("MyAppointments")]
        [Authorize(Roles ="USER, DOCTOR")]
        public async Task<ActionResult<IEnumerable<AppointmentResponseDTO>>> GetMyAppointments()
        {
            try
            {
                string role = User.FindFirst("role").Value;
                string email = User.FindFirst("email").Value;

                if (role == "DOCTOR")
                {
                    return Ok(await _appointmentsService.GetDoctorAppointmentsAsync(email));
                }
                else if (role == "USER")
                {

                    return Ok(await _appointmentsService.GetUserAppointmentsAsync(email));

                }
                else
                {
                    return Unauthorized();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}", Name = "GetById")]
        public async Task<ActionResult<AppointmentResponseDTO>> GetById(long id)
        {
            try
            {
                return Ok(await _appointmentsService.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "USER")]
        public async Task<ActionResult<AppointmentResponseDTO>> Post(AppointmentRequestDTO requestDTO)
        {
            try
            {
                long userId = long.Parse(User.FindFirst("id")!.Value);


                AppointmentResponseDTO responseDTO = await _appointmentsService.PostAsync(requestDTO, userId);
                return new CreatedAtRouteResult(
                                   routeName: "GetById",
                                   routeValues: new { id = responseDTO.Id },
                                   value: responseDTO
                                   );
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]

        public async Task<ActionResult<WorkerResponseDTO>> Put(long id, [FromBody] AppointmentUpdateDTO updateDTO)
        {
            try
            {
                AppointmentResponseDTO responseDTO = await _appointmentsService.PutAsync(id, updateDTO);
                return Ok(responseDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "WORKER")]

        public async Task<ActionResult<WorkerResponseDTO>> Delete(long id)
        {

            try
            {
                AppointmentResponseDTO responseDTO = await _appointmentsService.DeleteAsync(id);
                return Ok(responseDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
