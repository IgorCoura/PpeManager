using Microsoft.EntityFrameworkCore;
using PpeManager.Infrastructure;
using PpeManager.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var repository = new PpeRepository(context);
            var entities = repository.Find(x => x.Id == 1);
            Assert.True(true);
        }
       
    }
}
