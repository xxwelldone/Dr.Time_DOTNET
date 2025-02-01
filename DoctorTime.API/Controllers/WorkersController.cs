using DoctorTime.API.Context;
using DoctorTime.API.DTO.WorkerDTO;
using DoctorTime.API.Entities;
using DoctorTime.API.Repository.Interfaces;
using DoctorTime.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DoctorTime.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkersController : ControllerBase
    {
        private readonly IWorkerService _workerService;

        public WorkersController(IWorkerService workerService)
        {
            _workerService = workerService;
        }

        [HttpGet]
        [Authorize]

        public async Task<ActionResult<IEnumerable<WorkerResponseDTO>>> Get()
        {
            try
            {
                IEnumerable<WorkerResponseDTO> workerResponseDTO = await _workerService.GetAsync();
                return Ok(workerResponseDTO);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("{id}", Name = "GetWorkerById")]
        [Authorize]

        public async Task<ActionResult<WorkerResponseDTO>> GetById(long id)
        {
            try
            {
                WorkerResponseDTO worker = await _workerService.GetByIdAsync(id);
                return Ok(worker);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult<WorkerResponseDTO>> Post([FromBody] WorkerRequestDTO worker)
        {
            try
            {
                WorkerResponseDTO newWorker = await _workerService.PostAsync(worker);

                return new CreatedAtRouteResult(
                    routeName: "GetWorkerById",
                    routeValues: new { id = newWorker.Id },
                    value: newWorker
                    );
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        [Authorize]

        public async Task<ActionResult<WorkerResponseDTO>> Put(long id, [FromBody] WorkerUpdateDTO worker)
        {
            try
            {
                WorkerResponseDTO workerResponseDTO = await _workerService.PutAsync(id, worker);
                return Ok(workerResponseDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        [Authorize]

        public async Task<ActionResult<WorkerResponseDTO>> Delete(long id)
        {

            try
            {
                WorkerResponseDTO workerResponseDTO = await _workerService.DeleteAsync(id);
                return Ok(workerResponseDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
