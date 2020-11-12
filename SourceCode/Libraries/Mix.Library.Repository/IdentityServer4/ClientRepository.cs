using FreeSql;
using Mix.Data;
using Mix.Data.Repositories;
using Mix.Library.Entities.Databases.IdentityServer4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mix.Library.Repositories.IdentityServer4
{
    public class ClientRepository : AuditBaseRepository<Client>, IClientRepository
    {
        public ClientRepository(UnitOfWorkManager unitOfWorkManager, ICurrentUser currentUser) : base(unitOfWorkManager, currentUser)
        {
        }

        public async Task<Client> GetClientByIdAsync(string clientId)
        {
            var client = await Select.Where(t => t.ClientId == clientId).ToOneAsync();
            if (client is null) return null;

            client.AllowedCorsOrigins =
        }
    }
}