using Mix.Data.Repositories;
using Mix.Library.Entities.Databases.IdentityServer4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mix.Library.Repositories.IdentityServer4
{
    /// <summary>
    /// IClientRepository
    /// </summary>
    public interface IClientRepository : IAuditBaseRepository<Client>
    {
        /// <summary>
        /// Gets the client by identifier.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <returns></returns>
        Task<Client> GetClientByIdAsync(string clientId);
    }
}