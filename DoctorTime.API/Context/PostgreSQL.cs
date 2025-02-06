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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Doctor>(entity => entity.HasKey(e => e.Id));
            modelBuilder.Entity<Doctor>().Property(e => e.Id).ValueGeneratedOnAdd().IsRequired();
            modelBuilder.Entity<Doctor>().Property(e => e.Speciality).HasConversion<string>();
            modelBuilder.Entity<Doctor>().Property(e => e.Role).HasConversion<string>();
            modelBuilder.Entity<Doctor>().HasIndex(e => e.Email).IsUnique();
            modelBuilder.Entity<Doctor>().HasIndex(e => e.Crm).IsUnique();

            modelBuilder.Entity<Doctor>().HasMany(e => e.Appointments).WithOne(o => o.Doctor).HasForeignKey(o => o.DoctorId)
                 .OnDelete(DeleteBehavior.Restrict); ;

            modelBuilder.Entity<User>(entity => entity.HasKey(e => e.Id));
            modelBuilder.Entity<User>().Property(e => e.Id).ValueGeneratedOnAdd().IsRequired();
            modelBuilder.Entity<User>().Property(e => e.Role).HasConversion<string>();
            modelBuilder.Entity<User>().HasIndex(e => e.Email).IsUnique();
            modelBuilder.Entity<User>().HasIndex(e => e.Cpf).IsUnique();
            modelBuilder.Entity<User>().HasMany(e => e.Appointments).WithOne(o => o.User).HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Appointment>(entity => entity.HasKey(e => e.Id));
            modelBuilder.Entity<Appointment>().Property(e => e.Id).ValueGeneratedOnAdd().IsRequired();
            modelBuilder.Entity<Appointment>().Property(e => e.Status).HasConversion<string>();

            

            modelBuilder.Entity<Worker>(entity => entity.HasKey(e => e.Id));
            modelBuilder.Entity<Worker>().Property(e => e.Id).ValueGeneratedOnAdd().IsRequired();
            modelBuilder.Entity<Worker>().Property(e => e.Role).HasConversion<string>();
            modelBuilder.Entity<Worker>().HasIndex(e => e.Email).IsUnique();



            base.OnModelCreating(modelBuilder);
        }
    }


}

