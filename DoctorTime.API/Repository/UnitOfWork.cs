using DoctorTime.API.Context;
using DoctorTime.API.Repository.Interfaces;

namespace DoctorTime.API.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDoctorRepository _doctorRepository;

        private IWorkerRepository _workerRepository;
        private IUserRepository _userRepository;
        public PostgreSQL _context;

        public UnitOfWork(PostgreSQL context)
        {
            _context = context;
        }
        public IDoctorRepository DoctorRepository
        {
            get
            {
                return _doctorRepository = _doctorRepository ?? new DoctorRepository(_context);
            }
        }
        public IUserRepository UserRepository { get { return _userRepository = _userRepository ?? new UserRepository(_context); } }
        public IWorkerRepository WorkerRepository
        {
            get
            {
                return _workerRepository = _workerRepository ?? new WorkerRepository(_context);
            }
        }

        public async Task Commit()
        {
            await this._context.SaveChangesAsync();
        }
    }
}
