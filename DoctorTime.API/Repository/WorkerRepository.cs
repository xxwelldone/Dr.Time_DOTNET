using DoctorTime.API.Context;
using DoctorTime.API.Entities;
using DoctorTime.API.Repository.Interfaces;

namespace DoctorTime.API.Repository
{
    public class WorkerRepository : BaseRepository<Worker>, IWorkerRepository
    {
        public WorkerRepository(PostgreSQL context) : base(context)
        {
        }
    }
}
