﻿using PpeManager.Domain.Exceptions;

namespace PpeManager.Infrastructure.Idempotency
{
    public class RequestManager: IRequestManager
    {
        //private readonly PpeContext _context;

        public RequestManager()
        {
            //_context = context ?? throw new ArgumentNullException(nameof(context));
        }


        public async Task<bool> ExistAsync(Guid id)
        {
            /*
            var request = await _context.
                FindAsync<ClientRequest>(id);

            return request != null;
            */
            return true;
        }

        public async Task CreateRequestForCommandAsync<T>(Guid id)
        {
            /*
            var exists = await ExistAsync(id);

            var request = exists ?
                throw new PpeDomainException($"Request with {id} already exists") :
                new ClientRequest()
                {
                    Id = id,
                    Name = typeof(T).Name,
                    Time = DateTime.UtcNow
                };

            _context.Add(request);

            await _context.SaveChangesAsync();
            */
     

        }
    }
}
