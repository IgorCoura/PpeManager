using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace PpeManager.UnitTests.Application.Controller
{
    public class PpeControllerTest
    {
        private readonly Mock<IMediator> _mediatorMock;
        public PpeControllerTest()
        {
            _mediatorMock = new Mock<IMediator>();
        }

        [Fact]
        public async Task Create_ppe_success()
        {
            //Arrage
            var validCommand = new CreatePpeCommand("FakeCommand", "FakeCommand");
            var resultExpected = new PpeDTO(0, "FackDTO", "FackDTO");

            _mediatorMock.Setup(x => x.Send(It.IsAny<IdentifiedCommand<CreatePpeCommand, PpeDTO>>(), default(CancellationToken)))
                .Returns(Task.FromResult(resultExpected));
            //Act
            var ppeController = new PpeController(_mediatorMock.Object);
            var actionResult = await ppeController.CreatePpeAsync(validCommand, Guid.NewGuid().ToString());
            var result = actionResult.Result as CreatedResult ?? throw new ArgumentNullException();
            //Assert
            Assert.Equal((int)System.Net.HttpStatusCode.Created, result.StatusCode);
            Assert.Equal(resultExpected, result.Value);
        }

        [Fact]
        public async Task Create_ppe_with_invalid_requestId()
        {
            //Arrage
            var validCommand = new CreatePpeCommand("FakeCommand", "FakeCommand");
            var resultExpected = new PpeDTO(0, "FackDTO", "FackDTO");

            _mediatorMock.Setup(x => x.Send(It.IsAny<IdentifiedCommand<CreatePpeCommand, PpeDTO>>(), default(CancellationToken)))
                .Returns(Task.FromResult(resultExpected));
            //Act
            var ppeController = new PpeController(_mediatorMock.Object);
            var actionResult = await ppeController.CreatePpeAsync(validCommand, "");
            var result = actionResult.Result as BadRequestObjectResult?? throw new ArgumentNullException();
            //Assert
            Assert.Equal((int)System.Net.HttpStatusCode.BadRequest, result.StatusCode);
            Assert.Equal("Invalid request Id", result.Value);
            
        }

        [Fact]
        public async Task Create_ppe_bad_request()
        {
            //Arrage
            var command = new CreatePpeCommand("", "");

            _mediatorMock.Setup(x => x.Send(It.IsAny<IdentifiedCommand<CreatePpeCommand, PpeDTO>>(), default(CancellationToken)))
                .Throws(new Exception());
            //Act
            var ppeController = new PpeController(_mediatorMock.Object);
            var actionResult = await ppeController.CreatePpeAsync(command, Guid.NewGuid().ToString());
            var result = actionResult.Result as BadRequestObjectResult ?? throw new ArgumentNullException();
            //Assert
            Assert.Equal((int)System.Net.HttpStatusCode.BadRequest, result.StatusCode);
        }
    }
}
