using PpeManager.Api.Application.Commands.CreateWorkerCommand;
using PpeManager.Domain.AggregatesModel.AggregateCompany;
using PpeManager.Domain.AggregatesModel.AggregateWorker;

namespace PpeManager.UnitTests.Application.Commands
{
    public class CreateWorkerCommandHandlerTest
    {
        private readonly Mock<IWorkerRepository> _workerRepositoryMock;
        private readonly Mock<NotificationContext> _notificationMock;
        private readonly Mock<IConsultApprovalCertificateNumberService> _serviceMock;
        private readonly Mock<ICompanyRepository> _companyRepositoryMock;


        public CreateWorkerCommandHandlerTest()
        {
            _workerRepositoryMock = new Mock<IWorkerRepository>();
            _notificationMock = new Mock<NotificationContext>();
            _serviceMock = new Mock<IConsultApprovalCertificateNumberService>();
            _companyRepositoryMock = new Mock<ICompanyRepository>();

        }

        [Fact]
        public async Task Handler_return_WorkerDto_if_worker_is_persisted()
        {
            //Arrange
            var fakeCommand = new CreateWorkerCommand("fakeCommand", "role", "092.444.670-62", "12345", "12/12/12", 0);
            var company = new Company("fakeCommand", "73.706.750/0001-57");
            var entity = new Worker(fakeCommand.Name, fakeCommand.Role, fakeCommand.Cpf, fakeCommand.RegistrationNumber, DateOnly.Parse(fakeCommand.AdmissionDate).ToString(), company);
            var expectedResult = WorkerDTO.FromEntity(entity);

            _workerRepositoryMock.Setup(repo => repo.Add(It.IsAny<Worker>())).Returns(entity);
            _companyRepositoryMock.Setup(repo => repo.Find(It.IsAny<Func<Company, bool>>())).Returns(company);
            _workerRepositoryMock.Setup(repo => repo.UnitOfWork.SaveEntitiesAsync(It.IsAny<CancellationToken>()));


            //Act
            var handler = new CreateWorkerCommandHandler(_notificationMock.Object, _workerRepositoryMock.Object, _companyRepositoryMock.Object);
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
            var fakeCommand = new CreateWorkerCommand("", "", "", "", "12/12/12", -1);


            //Act
            var handler = new CreateWorkerCommandHandler(_notificationMock.Object, _workerRepositoryMock.Object, _companyRepositoryMock.Object);
            var cltToken = new System.Threading.CancellationToken();


            //Asssert 
            await Assert.ThrowsAsync<WorkerDomainException>(() => handler.Handle(fakeCommand, cltToken));
            Assert.False(_notificationMock.Object.IsValid);
        }
    }
}
