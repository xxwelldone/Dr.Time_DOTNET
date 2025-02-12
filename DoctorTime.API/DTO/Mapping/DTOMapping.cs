﻿using AutoMapper;
using DoctorTime.API.DTO.AppointmentDTO;
using DoctorTime.API.DTO.DoctorDTO;
using DoctorTime.API.DTO.UserDTO;
using DoctorTime.API.DTO.WorkerDTO;
using DoctorTime.API.Entities;

namespace DoctorTime.API.DTO.Mapping
{
    public class DTOMapping : Profile
    {
        public DTOMapping()
        {
            CreateMap<User, UserResponseDTO>();
            CreateMap<UserRequestDTO, User>().ReverseMap();
            //CreateMap<UserUpdateDTO, User>();

            CreateMap<Doctor, DoctorResposeDTO>();
            CreateMap<DoctorRequestDTO, Doctor>();
            //CreateMap<DoctorUpdateDTO, Doctor>();

            CreateMap<Worker, WorkerResponseDTO>();
            CreateMap<WorkerRequestDTO, Worker>();

            CreateMap<Appointment, AppointmentResponseDTO>();
            CreateMap<AppointmentRequestDTO, Appointment>();


        }
    }
}
