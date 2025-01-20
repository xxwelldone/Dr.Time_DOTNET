using DoctorTime.API.Context;
using DoctorTime.API.Entities;
using DoctorTime.API.Repository.Interfaces;
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
        private readonly IUnitOfWork _unitOfWork;

        public WorkersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Worker>>> Get()
        {

            IEnumerable<Worker> workers = await _unitOfWork.WorkerRepository.GetAllAsync();
            return Ok(workers);
        }
        [HttpGet("{id}", Name = "GetWorkerById")]
        public async Task<ActionResult<Worker>> GetById(long id)
        {
            Worker worker = await _unitOfWork.WorkerRepository.GetByExpression(x => x.Id == id);
            return Ok(worker);
        }
        [HttpPost]
        public async Task<ActionResult<Worker>> Post([FromBody] Worker worker)
        {
            Worker newWorker = await _unitOfWork.WorkerRepository.Create(worker);
            await _unitOfWork.Commit();
            return new CreatedAtRouteResult(
                routeName: "GetWorkerById",
                routeValues: new { id = newWorker.Id },
                value: newWorker
                );
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Worker>> Put(long id, [FromBody] Worker worker)
        {
            if (id != worker.Id)
            {
                return BadRequest();
            }
            _unitOfWork.WorkerRepository.Update(worker);
            await _unitOfWork.Commit();


            return Ok(worker);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Worker>> Delete(long id)
        {
            Worker worker = await _unitOfWork.WorkerRepository.GetByExpression(x => x.Id == id);
            if (worker is null)
            {
                return NotFound();
            }
            _unitOfWork.WorkerRepository.Delete(worker);
            await _unitOfWork.Commit();
            return Ok(worker);

        }
    }
}
