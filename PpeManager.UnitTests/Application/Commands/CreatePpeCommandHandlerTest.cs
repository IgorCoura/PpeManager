namespace PpeManager.UnitTests.Application.Commands
{
    public class CreatePpeCommandHandlerTest
    {
        private readonly Mock<IPpeRepository> _ppeRepositoryMock;
        private readonly Mock<NotificationContext> _notificationMock;


        public CreatePpeCommandHandlerTest()
        {
            _ppeRepositoryMock = new Mock<IPpeRepository>();
            _notificationMock = new Mock<NotificationContext>();
        }

        [Fact]
        public async Task Handler_return_ppeDto_if_ppe_is_persisted()
        {
            //Arrange
            var fakeCommand = new CreatePpeCommand("fakePpe", "FakeDescription");
            var entity = new Ppe(fakeCommand.Name, fakeCommand.Description);
            var expectedResult =PpeDTO.FromEntity(entity);  

            _ppeRepositoryMock.Setup(repo => repo.Add(It.IsAny<Ppe>())).Returns(entity);

            //Act
            var handler = new CreatePpeCommandHandler( _notificationMock.Object, _ppeRepositoryMock.Object);
            var cltToken = new System.Threading.CancellationToken();
            var result = await handler.Handle(fakeCommand, cltToken);

            //Asssert 
            Assert.True(_notificationMock.Object.IsValid);
            Assert.True(expectedResult.Equal(result));
        }
        [Fact]
        public async Task Hadler_return_PpeDomainException_if_ppe_is_invalid()
        {
            //Arrange
            var fakeCommand = new CreatePpeCommand("", "");

            //Act
            var handler = new CreatePpeCommandHandler( _notificationMock.Object, _ppeRepositoryMock.Object);
            var cltToken = new System.Threading.CancellationToken();

            //Asssert 
            await Assert.ThrowsAsync<PpeDomainException>(() => handler.Handle(fakeCommand, cltToken));
            Assert.False(_notificationMock.Object.IsValid);
        }



    }
}
