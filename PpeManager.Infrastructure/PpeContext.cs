using Microsoft.EntityFrameworkCore;
using PpeManager.Domain.Seedwork;

namespace PpeManager.Infrastructure
{
    public class PpeContext : DbContext, IUnitOfWork
    {
        public Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}