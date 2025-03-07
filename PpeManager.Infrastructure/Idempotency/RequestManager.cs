﻿
using Microsoft.eShopOnContainers.Services.Ordering.Infrastructure.Idempotency;

namespace PpeManager.Infrastructure.Idempotency
{
    public class RequestManager : IRequestManager
    {
        private readonly PpeManagerContext _context;

        public RequestManager(PpeManagerContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }


        public async Task<bool> ExistAsync(Guid id)
        {

            var request = await _context.
                FindAsync<ClientRequest>(id);

            return request != null;
        }

        public async Task CreateRequestForCommandAsync<T>(Guid id)
        {
            var aa = DateTime.UtcNow.AddDays(8) > DateTime.UtcNow.AddDays(7);
            _context.ClientRequest.RemoveRange(_context.ClientRequest.Where(x => x.Time > DateTime.UtcNow.AddDays(7)));

            var exists = await ExistAsync(id);

            var request = exists ?
                throw new PpeDomainException($"Request with {id} already exists") :
                new ClientRequest()
                {
                    Id = id,
                    Name = typeof(T).Name,
                    Time = DateTime.UtcNow
                };

            //_context.Add(request);

            //await _context.SaveChangesAsync();



        }
    }
}
