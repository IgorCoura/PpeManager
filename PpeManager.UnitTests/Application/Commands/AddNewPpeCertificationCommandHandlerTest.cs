﻿namespace PpeManager.UnitTests.Application.Commands
{
    public class AddNewPpeCertificationCommandHandlerTest
    {
        private readonly Mock<IPpeRepository> _ppeRepositoryMock;
        private readonly Mock<NotificationContext> _notificationMock;
        private readonly Mock<IConsultApprovalCertificateNumberService> _serviceMock;


        public AddNewPpeCertificationCommandHandlerTest()
        {
            _ppeRepositoryMock = new Mock<IPpeRepository>();
            _notificationMock = new Mock<NotificationContext>();
            _serviceMock = new Mock<IConsultApprovalCertificateNumberService>();

        }

        [Fact]
        public async Task Handler_return_ppeDto_if_ppeCertification_is_persisted()
        {
            //Arrange
            var fakeCommand = new AddNewPpeCertificationCommand(0, "31469", 5);
            var entity = new Ppe("Ppe", "PpeDescription");
            var entityWithoutCertification = new Ppe("Ppe", "PpeDescription");
            entity.addCertification(new PpeCertification( fakeCommand.ApprovalCertificateNumber, DateOnly.MaxValue, fakeCommand.Durability));
            var expectedResult = PpeDTO.FromEntity(entity);

            _ppeRepositoryMock.Setup(repo => repo.Find(It.IsAny<Func<Ppe, bool>>())).Returns(entityWithoutCertification);
            _ppeRepositoryMock.Setup(repo => repo.Update(It.IsAny<Ppe>())).Returns(entity);
            _ppeRepositoryMock.Setup(repo => repo.UnitOfWork.SaveEntitiesAsync(It.IsAny<CancellationToken>()));
            _serviceMock.Setup(s => s.ConsultValidity(It.IsAny<ApprovalCertificate>())).Returns(DateOnly.MaxValue);

            //Act
            var handler = new AddNewPpeCertificationCommandHandler(_ppeRepositoryMock.Object, _notificationMock.Object, _serviceMock.Object);
            var cltToken = new System.Threading.CancellationToken();
            var result = await handler.Handle(fakeCommand, cltToken);

            //Asssert 
            Assert.True(_notificationMock.Object.IsValid);
            Assert.True(expectedResult.Equal(result));
        }
        [Fact]
        public async Task Hadler_return_PpeDomainException_if_ppeCertification_with_invalid_number()
        {
            //Arrange
            var fakeCommand = new AddNewPpeCertificationCommand(0, "", -1);
            var entity = new Ppe("Ppe", "PpeDescription");

            _ppeRepositoryMock.Setup(repo => repo.Find(It.IsAny<Func<Ppe, bool>>())).Returns(entity);
            _serviceMock.Setup(s => s.ConsultValidity(It.IsAny<ApprovalCertificate>())).Returns(DateOnly.MinValue);

            //Act
            var handler = new AddNewPpeCertificationCommandHandler(_ppeRepositoryMock.Object, _notificationMock.Object, _serviceMock.Object);
            var cltToken = new System.Threading.CancellationToken();


            //Asssert 
            await Assert.ThrowsAsync<PpeDomainException>(() => handler.Handle(fakeCommand, cltToken));
            Assert.False(_notificationMock.Object.IsValid);
        }

    }
}
