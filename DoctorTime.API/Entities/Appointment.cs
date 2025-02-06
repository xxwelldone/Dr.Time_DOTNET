using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using DoctorTime.API.DTO.AppointmentDTO;
using DoctorTime.API.DTO.DoctorDTO;
using DoctorTime.API.Entities.Enums;
using Microsoft.EntityFrameworkCore.Update.Internal;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace DoctorTime.API.Entities
{
    public class Appointment : BaseEntity
    {

        public DateTime DateTime { get; set; }

        public string Modality { get; set; }
        public Status Status { get; set; }

        public long DoctorId { get; set; }
        public Doctor? Doctor { get; set; }

        public long UserId { get; set; }
        public User? User { get; set; }



        public Appointment(long id, long userId, User user, long doctorId, Doctor doctor, DateTime dateTime, string modality, Status status) : base(id)
        {
            User = user;
            UserId = userId;
            Doctor = doctor;
            DoctorId = doctorId;
            DateTime = dateTime;
            Modality = modality;
            Status = status;
        }

        public Appointment()
        {
        }

        public void Update(AppointmentUpdateDTO updateDTO)
        {
            if (updateDTO.Status != null)
            {
                Status = (Status)updateDTO.Status;
            }

            if (updateDTO.Modality != null)
            {
                Modality = updateDTO.Modality;
            }
            if (updateDTO.DateTime != null)
            {
                DateTime = (DateTime)updateDTO.DateTime;
            }

        }
    }
}
