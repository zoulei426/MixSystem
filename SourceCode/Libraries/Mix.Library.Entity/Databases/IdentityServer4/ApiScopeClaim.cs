using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mix.Library.Entities.Databases.IdentityServer4
{
    /// <summary>
    /// ApiScopeClaim
    /// </summary>
    /// <seealso cref="Mix.Library.Entities.Databases.IdentityServer4.UserClaim" />
    public class ApiScopeClaim : UserClaim
    {
        /// <summary>
        /// Gets or sets the API scope identifier.
        /// </summary>
        /// <value>
        /// The API scope identifier.
        /// </value>
        public int ApiScopeId { get; set; }

        /// <summary>
        /// Gets or sets the API scope.
        /// </summary>
        /// <value>
        /// The API scope.
        /// </value>
        public ApiScope ApiScope { get; set; }
    }
}