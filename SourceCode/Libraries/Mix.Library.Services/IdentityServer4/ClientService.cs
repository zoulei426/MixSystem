using IdentityServer4.Models;
using Mix.Data.Repositories;
using Mix.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mix.Library.Services.IdentityServer4
{
    public class ClientService : ApplicationService
    {
        private readonly IAuditBaseRepository<Entities.Databases.IdentityServer4.Client> clientRepository;
        private readonly IAuditBaseRepository<Entities.Databases.IdentityServer4.ClientCorsOrigin> clientCorsOriginRepository;

        public ClientService(
            IAuditBaseRepository<Entities.Databases.IdentityServer4.Client> clientRepository,
            IAuditBaseRepository<Entities.Databases.IdentityServer4.ClientCorsOrigin> clientCorsOriginRepository)
        {
            this.clientRepository = clientRepository;
            this.clientCorsOriginRepository = clientCorsOriginRepository;
        }

        public async Task<Client> GetClientAsync(string clientId)
        {
            var client = await clientRepository.Select.Where(t => t.ClientId == clientId).ToOneAsync();
            if (client is null) return null;

            client.AllowedCorsOrigins = clientCorsOriginRepository.Select.Where(t => t.ClientId == clientId).ToListAsync();
        }
    }
}