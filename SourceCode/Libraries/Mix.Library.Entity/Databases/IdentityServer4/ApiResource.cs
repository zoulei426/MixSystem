using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mix.Library.Entities.Databases.IdentityServer4
{
    /// <summary>
    /// ApiResource
    /// </summary>
    public class ApiResource
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ApiResource"/> is enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if enabled; otherwise, <c>false</c>.
        /// </value>
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>
        /// The display name.
        /// </value>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the allowed access token signing algorithms.
        /// </summary>
        /// <value>
        /// The allowed access token signing algorithms.
        /// </value>
        public string AllowedAccessTokenSigningAlgorithms { get; set; }

        /// <summary>
        /// Gets or sets the secrets.
        /// </summary>
        /// <value>
        /// The secrets.
        /// </value>
        public List<ApiSecret> Secrets { get; set; }

        /// <summary>
        /// Gets or sets the scopes.
        /// </summary>
        /// <value>
        /// The scopes.
        /// </value>
        public List<ApiScope> Scopes { get; set; }

        /// <summary>
        /// Gets or sets the user claims.
        /// </summary>
        /// <value>
        /// The user claims.
        /// </value>
        public List<ApiResourceClaim> UserClaims { get; set; }

        /// <summary>
        /// Gets or sets the properties.
        /// </summary>
        /// <value>
        /// The properties.
        /// </value>
        public List<ApiResourceProperty> Properties { get; set; }

        /// <summary>
        /// Gets or sets the created.
        /// </summary>
        /// <value>
        /// The created.
        /// </value>
        public DateTime Created { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the updated.
        /// </summary>
        /// <value>
        /// The updated.
        /// </value>
        public DateTime? Updated { get; set; }

        /// <summary>
        /// Gets or sets the last accessed.
        /// </summary>
        /// <value>
        /// The last accessed.
        /// </value>
        public DateTime? LastAccessed { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [non editable].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [non editable]; otherwise, <c>false</c>.
        /// </value>
        public bool NonEditable { get; set; }
    }
}