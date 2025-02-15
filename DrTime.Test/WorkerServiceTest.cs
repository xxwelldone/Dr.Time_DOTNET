using AutoMapper;
using DoctorTime.API.Context;
using DoctorTime.API.Controllers;
using DoctorTime.API.Entities;
using DoctorTime.API.Repository.Interfaces;
using DoctorTime.API.Security.Interfaces;
using DoctorTime.API.Services;
using DoctorTime.API.Services.Interfaces;
using Moq;
using System.Linq.Expressions;

namespace DrTime.Test
{
    public class WorkerServiceTest
    {
        Mock<IUnitOfWork> uow = new Mock<IUnitOfWork>();
        Mock<IMapper> imapper = new Mock<IMapper>();
        Mock<ILoginService> ilogin = new Mock<ILoginService>();
        IWorkerService workerService;


        [Fact]
        public async Task DeleteAsync_ShouldThrowErrorAsync()
        {
            //Arrange
            uow.Setup(x => x.WorkerRepository.GetByExpression(It.IsAny<Expression<Func<Worker, bool>>>()))
                .ReturnsAsync((Worker)null);


            workerService = new WorkerService(uow.Object, imapper.Object, ilogin.Object);

            await Assert.ThrowsAsync<Exception>(() => workerService.DeleteAsync(1L));


        }
    }
}