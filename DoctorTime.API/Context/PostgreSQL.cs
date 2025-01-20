using DoctorTime.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace DoctorTime.API.Context
{
    public class PostgreSQL : DbContext
    {
        public PostgreSQL(DbContextOptions<PostgreSQL> options) : base(options)
        {
        }
        public DbSet<Worker> Workers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
    }
}
