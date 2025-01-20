﻿namespace DoctorTime.API.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        IDoctorRepository  DoctorRepository { get; }
        IWorkerRepository WorkerRepository { get; }
        IUserRepository UserRepository { get; }
        Task Commit();

    }
}
