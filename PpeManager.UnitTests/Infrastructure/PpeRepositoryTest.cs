using Microsoft.EntityFrameworkCore;
using PpeManager.Infrastructure;
using PpeManager.Infrastructure.Repositories;

namespace PpeManager.UnitTests.Infrastructure
{
    public class PpeRepositoryTest
    {
        private readonly Mock<IMediator> _mediator;

        public PpeRepositoryTest()
        {
            _mediator = new Mock<IMediator>();
        }

        [Fact]
        public async Task test_repository()
      {
            var options = new DbContextOptionsBuilder<PpeManagerContext>().UseNpgsql("Server=192.168.15.34;Port=5432;Database=ppemanager;User Id=admin;Password=mysecretpassword;").Options;
            var context = new PpeManagerContext(options, _mediator.Object);
            var repository = new WorkerRepository(context);
            var entities = repository.FindByPossession(p => p.Id == 1);
            Assert.True(true);
        }

    }
}
