using PpeManager.Api.Application.Commands.CreateWorkerCommand;
using PpeManager.Domain.AggregatesModel.AggregateCompany;
using PpeManager.Domain.AggregatesModel.AggregateWorker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PpeManager.UnitTests.Application.Commands
{
    public class CreateWorkerCommandHandlerTest
    {
        private readonly Mock<IWorkerRepository> _workerRepositoryMock;
        private readonly Mock<NotificationContext> _notificationMock;
        private readonly Mock<IConsultApprovalCertificateNumberService> _serviceMock;


        public CreateWorkerCommandHandlerTest()
        {
            _workerRepositoryMock = new Mock<IWorkerRepository>();
            _notificationMock = new Mock<NotificationContext>();
            _serviceMock = new Mock<IConsultApprovalCertificateNumberService>();

        }

        [Fact]
        public async Task Handler_return_WorkerDto_if_worker_is_persisted()
        {
            //Arrange
            var fakeCommand = new CreateWorkerCommand("fakeCommand", "role", "12345", "12/12/12", 0);
            var entity = new Worker(fakeCommand.Name, fakeCommand.Role, fakeCommand.RegistrationNumber ,DateOnly.Parse(fakeCommand.AdmissionDate), fakeCommand.CompanyId);
            var company = new Company("fakeCommand", "02624917000192");
            entity.setCompany(company);
            var expectedResult = new WorkerDTO(0, entity.Name.ToString(), entity.Role, entity.RegistrationNumber, entity.AdmissionDate.ToString(), new CompanyDTO(0, "fakeCommand", "02624917000192"));

            _workerRepositoryMock.Setup(repo => repo.Add(It.IsAny<Worker>())).Returns(entity);

            //Act
            var handler = new CreateWorkerCommandHandler(_notificationMock.Object, _workerRepositoryMock.Object);
            var cltToken = new System.Threading.CancellationToken();
            var result = await handler.Handle(fakeCommand, cltToken);

            //Asssert 
            Assert.True(_notificationMock.Object.IsValid);
            Assert.True(expectedResult.Equal(result));
        }
        [Fact]
        public async Task Hadler_return_WorkerDomainException_if_worker_is_invalid()
        {
            //Arrange
            var fakeCommand = new CreateWorkerCommand("", "","","12/12/12",-1);


            //Act
            var handler = new CreateWorkerCommandHandler(_notificationMock.Object, _workerRepositoryMock.Object);
            var cltToken = new System.Threading.CancellationToken();


            //Asssert 
            await Assert.ThrowsAsync<WorkerDomainException>(() => handler.Handle(fakeCommand, cltToken));
            Assert.False(_notificationMock.Object.IsValid);
        }
    }
}
