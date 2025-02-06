using AutoMapper;
using DoctorTime.API.DTO.AppointmentDTO;
using DoctorTime.API.DTO.DoctorDTO;
using DoctorTime.API.DTO.UserDTO;
using DoctorTime.API.DTO.WorkerDTO;
using DoctorTime.API.Entities;
using DoctorTime.API.Repository.Interfaces;
using DoctorTime.API.Services.Interfaces;

namespace DoctorTime.API.Services
{
    public class AppointmentService : IAppointmentService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AppointmentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<AppointmentResponseDTO> DeleteAsync(long id)
        {
            Appointment appointment = await _unitOfWork.AppointmentRepository.GetByExpression(x => x.Id == id);
            if (appointment == null)
            {
                throw new Exception("No appointment found");
            }
            _unitOfWork.AppointmentRepository.Delete(appointment);
            await _unitOfWork.Commit();
            AppointmentResponseDTO appointmentResponseDTO = _mapper.Map<AppointmentResponseDTO>(appointment);
            return appointmentResponseDTO;
        }

        public async Task<IEnumerable<AppointmentResponseDTO>> GetAllAsync()
        {
            IEnumerable<Appointment> appointments = await _unitOfWork.AppointmentRepository.GetAllAsync();
            IEnumerable<AppointmentResponseDTO> appointmentResponseDTOs = _mapper.Map<IEnumerable<AppointmentResponseDTO>>(appointments);
            return appointmentResponseDTOs;
        }

        public async Task<AppointmentResponseDTO> GetByIdAsync(long id)
        {
            Appointment appointment = await _unitOfWork.AppointmentRepository.GetByExpression(x => x.Id == id);
            AppointmentResponseDTO appointmentResponseDTO = _mapper.Map<AppointmentResponseDTO>(appointment);
            return appointmentResponseDTO;
        }

        public async Task<IEnumerable<AppointmentResponseDTO>> GetDoctorAppointmentsAsync(string email)
        {
            IEnumerable<Appointment> appointments = await _unitOfWork.AppointmentRepository.GetAllMyDoctorAppoinments(email);
            IEnumerable<AppointmentResponseDTO> appointmentResponseDTOs = _mapper.Map<IEnumerable<AppointmentResponseDTO>>(appointments);
            return appointmentResponseDTOs;
        }

        public async Task<IEnumerable<AppointmentResponseDTO>> GetUserAppointmentsAsync(string email)
        {
            IEnumerable<Appointment> appointments = await _unitOfWork.AppointmentRepository.GetAllMyUserAppoinments(email);
            IEnumerable<AppointmentResponseDTO> appointmentResponseDTOs = _mapper.Map<IEnumerable<AppointmentResponseDTO>>(appointments);
            return appointmentResponseDTOs;
        }

        public async Task<AppointmentResponseDTO> PostAsync(AppointmentRequestDTO request, long userId)
        {
            User user = await _unitOfWork.UserRepository.GetByExpression(user => user.Id == userId);
            Doctor doctor = await _unitOfWork.DoctorRepository.GetByExpression(doctor => doctor.Id == request.DoctorId);

            Appointment appointment = _mapper.Map<Appointment>(request);
            appointment.User = user;
            appointment.UserId = user.Id;
            appointment.Doctor = doctor;
            appointment.DoctorId = request.DoctorId;

            await _unitOfWork.AppointmentRepository.Create(appointment);
            await _unitOfWork.Commit();
            AppointmentResponseDTO appointmentResponse = _mapper.Map<AppointmentResponseDTO>(appointment);
            return appointmentResponse;
        }

        public async Task<AppointmentResponseDTO> PutAsync(long id, AppointmentUpdateDTO updateDTO)
        {
            Appointment appointment = await _unitOfWork.AppointmentRepository.GetByExpression(x => x.Id == id);
            if (appointment == null)
            {
                throw new Exception("Appointment not found");
            }
            appointment.Update(updateDTO);
            _unitOfWork.AppointmentRepository.Update(appointment);
            await _unitOfWork.Commit();
            AppointmentResponseDTO responseDTO = _mapper.Map<AppointmentResponseDTO>(appointment);
            return responseDTO;
        }
    }
}
