using PpeManager.Api.Application.Commands.CreateCompanyCommand;
using PpeManager.Domain.AggregatesModel.AggregateCompany;

namespace PpeManager.UnitTests.Application.Commands
{
    public class CreateCompanyCommandHandlerTest
    {
        private readonly Mock<ICompanyRepository> _companyRepositoryMock;
        private readonly Mock<NotificationContext> _notificationMock;
        private readonly Mock<IConsultApprovalCertificateNumberService> _serviceMock;


        public CreateCompanyCommandHandlerTest()
        {
            _companyRepositoryMock = new Mock<ICompanyRepository>();
            _notificationMock = new Mock<NotificationContext>();
            _serviceMock = new Mock<IConsultApprovalCertificateNumberService>();

        }

        [Fact]
        public async Task Handler_return_companyDto_if_company_is_persisted()
        {
            //Arrange
            var fakeCommand = new CreateCompanyCommand("fakeCommand", "02624917000192");
            var entity = new Company(fakeCommand.Name, fakeCommand.Cnpj);
            var expectedResult = new CompanyDTO(0, entity.Name.ToString(), entity.Cnpj.ToString());

            _companyRepositoryMock.Setup(repo => repo.Add(It.IsAny<Company>())).Returns(entity);
            _companyRepositoryMock.Setup(repo => repo.UnitOfWork.SaveEntitiesAsync(It.IsAny<CancellationToken>()));

            //Act
            var handler = new CreateCompanyCommandHandler(_notificationMock.Object, _companyRepositoryMock.Object);
            var cltToken = new System.Threading.CancellationToken();
            var result = await handler.Handle(fakeCommand, cltToken);

            //Asssert 
            Assert.True(_notificationMock.Object.IsValid);
            Assert.Equal(expectedResult, result);
        }
        [Fact]
        public async Task Hadler_return_CompanyDomainException_if_company_is_invalid()
        {
            //Arrange
            var fakeCommand = new CreateCompanyCommand("", "");


            //Act
            var handler = new CreateCompanyCommandHandler(_notificationMock.Object, _companyRepositoryMock.Object);
            var cltToken = new System.Threading.CancellationToken();


            //Asssert 
            await Assert.ThrowsAsync<CompanyDomainException>(() => handler.Handle(fakeCommand, cltToken));
            Assert.False(_notificationMock.Object.IsValid);
        }

    }


}
