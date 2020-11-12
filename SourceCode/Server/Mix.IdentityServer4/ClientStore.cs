using FreeSql;
using Grpc.Core.Logging;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using Microsoft.Extensions.Logging;
using Mix.Library.DbContexts.IdentityServer4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mix.IdentityServer4
{
    public class ClientStore : IClientStore
    {
        private readonly IConfigurationDbContext context;
        private readonly ILogger<ClientStore> logger;

        public ClientStore(IConfigurationDbContext context, ILogger<ClientStore> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public async Task<Client> FindClientByIdAsync(string clientId)
        {
            ISelect<Library.Entities.Databases.IdentityServer4.Client> baseQuery = context.Clients
                .Where(x => x.ClientId == clientId)
                .Take(1);

            var client = await baseQuery.ToOneAsync();
            if (client == null) return null;

            await baseQuery.Include(x => x.AllowedCorsOrigins).IncludeMany(c => c.AllowedCorsOrigins).ToListAsync();
            await baseQuery.Include(x => x.AllowedGrantTypes).IncludeMany(c => c.AllowedGrantTypes).ToListAsync();
            await baseQuery.Include(x => x.AllowedScopes).IncludeMany(c => c.AllowedScopes).ToListAsync();
            await baseQuery.Include(x => x.Claims).IncludeMany(c => c.Claims).ToListAsync();
            await baseQuery.Include(x => x.ClientSecrets).IncludeMany(c => c.ClientSecrets).ToListAsync();
            await baseQuery.Include(x => x.IdentityProviderRestrictions).IncludeMany(c => c.IdentityProviderRestrictions).ToListAsync();
            await baseQuery.Include(x => x.PostLogoutRedirectUris).IncludeMany(c => c.PostLogoutRedirectUris).ToListAsync();
            await baseQuery.Include(x => x.Properties).IncludeMany(c => c.Properties).ToListAsync();
            await baseQuery.Include(x => x.RedirectUris).IncludeMany(c => c.RedirectUris).ToListAsync();

            var model = client.ToModel();

            logger.LogDebug("{clientId} found in database: {clientIdFound}", clientId, model != null);

            return model;
        }
    }
}