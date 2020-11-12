using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mix.Library.Entities.Databases.IdentityServer4
{
    /// <summary>
    /// Client
    /// </summary>
    public class Client
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Client"/> is enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if enabled; otherwise, <c>false</c>.
        /// </value>
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// Gets or sets the client identifier.
        /// </summary>
        /// <value>
        /// The client identifier.
        /// </value>
        public string ClientId { get; set; }

        /// <summary>
        /// Gets or sets the type of the protocol.
        /// </summary>
        /// <value>
        /// The type of the protocol.
        /// </value>
        public string ProtocolType { get; set; } = "oidc";

        /// <summary>
        /// Gets or sets the client secrets.
        /// </summary>
        /// <value>
        /// The client secrets.
        /// </value>
        public List<ClientSecret> ClientSecrets { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [require client secret].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [require client secret]; otherwise, <c>false</c>.
        /// </value>
        public bool RequireClientSecret { get; set; } = true;

        /// <summary>
        /// Gets or sets the name of the client.
        /// </summary>
        /// <value>
        /// The name of the client.
        /// </value>
        public string ClientName { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the client URI.
        /// </summary>
        /// <value>
        /// The client URI.
        /// </value>
        public string ClientUri { get; set; }

        /// <summary>
        /// Gets or sets the logo URI.
        /// </summary>
        /// <value>
        /// The logo URI.
        /// </value>
        public string LogoUri { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [require consent].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [require consent]; otherwise, <c>false</c>.
        /// </value>
        public bool RequireConsent { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether [allow remember consent].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [allow remember consent]; otherwise, <c>false</c>.
        /// </value>
        public bool AllowRememberConsent { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether [always include user claims in identifier token].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [always include user claims in identifier token]; otherwise, <c>false</c>.
        /// </value>
        public bool AlwaysIncludeUserClaimsInIdToken { get; set; }

        /// <summary>
        /// Gets or sets the allowed grant types.
        /// </summary>
        /// <value>
        /// The allowed grant types.
        /// </value>
        public List<ClientGrantType> AllowedGrantTypes { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [require pkce].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [require pkce]; otherwise, <c>false</c>.
        /// </value>
        public bool RequirePkce { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether [allow plain text pkce].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [allow plain text pkce]; otherwise, <c>false</c>.
        /// </value>
        public bool AllowPlainTextPkce { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [require request object].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [require request object]; otherwise, <c>false</c>.
        /// </value>
        public bool RequireRequestObject { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [allow access tokens via browser].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [allow access tokens via browser]; otherwise, <c>false</c>.
        /// </value>
        public bool AllowAccessTokensViaBrowser { get; set; }

        /// <summary>
        /// Gets or sets the redirect uris.
        /// </summary>
        /// <value>
        /// The redirect uris.
        /// </value>
        public List<ClientRedirectUri> RedirectUris { get; set; }

        /// <summary>
        /// Gets or sets the post logout redirect uris.
        /// </summary>
        /// <value>
        /// The post logout redirect uris.
        /// </value>
        public List<ClientPostLogoutRedirectUri> PostLogoutRedirectUris { get; set; }

        /// <summary>
        /// Gets or sets the front channel logout URI.
        /// </summary>
        /// <value>
        /// The front channel logout URI.
        /// </value>
        public string FrontChannelLogoutUri { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [front channel logout session required].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [front channel logout session required]; otherwise, <c>false</c>.
        /// </value>
        public bool FrontChannelLogoutSessionRequired { get; set; } = true;

        /// <summary>
        /// Gets or sets the back channel logout URI.
        /// </summary>
        /// <value>
        /// The back channel logout URI.
        /// </value>
        public string BackChannelLogoutUri { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [back channel logout session required].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [back channel logout session required]; otherwise, <c>false</c>.
        /// </value>
        public bool BackChannelLogoutSessionRequired { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether [allow offline access].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [allow offline access]; otherwise, <c>false</c>.
        /// </value>
        public bool AllowOfflineAccess { get; set; }

        /// <summary>
        /// Gets or sets the allowed scopes.
        /// </summary>
        /// <value>
        /// The allowed scopes.
        /// </value>
        public List<ClientScope> AllowedScopes { get; set; }

        /// <summary>
        /// Gets or sets the identity token lifetime.
        /// </summary>
        /// <value>
        /// The identity token lifetime.
        /// </value>
        public int IdentityTokenLifetime { get; set; } = 300;

        /// <summary>
        /// Gets or sets the allowed identity token signing algorithms.
        /// </summary>
        /// <value>
        /// The allowed identity token signing algorithms.
        /// </value>
        public string AllowedIdentityTokenSigningAlgorithms { get; set; }

        /// <summary>
        /// Gets or sets the access token lifetime.
        /// </summary>
        /// <value>
        /// The access token lifetime.
        /// </value>
        public int AccessTokenLifetime { get; set; } = 3600;

        /// <summary>
        /// Gets or sets the authorization code lifetime.
        /// </summary>
        /// <value>
        /// The authorization code lifetime.
        /// </value>
        public int AuthorizationCodeLifetime { get; set; } = 300;

        /// <summary>
        /// Gets or sets the consent lifetime.
        /// </summary>
        /// <value>
        /// The consent lifetime.
        /// </value>
        public int? ConsentLifetime { get; set; } = null;

        /// <summary>
        /// Gets or sets the absolute refresh token lifetime.
        /// </summary>
        /// <value>
        /// The absolute refresh token lifetime.
        /// </value>
        public int AbsoluteRefreshTokenLifetime { get; set; } = 2592000;

        /// <summary>
        /// Gets or sets the sliding refresh token lifetime.
        /// </summary>
        /// <value>
        /// The sliding refresh token lifetime.
        /// </value>
        public int SlidingRefreshTokenLifetime { get; set; } = 1296000;

        /// <summary>
        /// Gets or sets the refresh token usage.
        /// </summary>
        /// <value>
        /// The refresh token usage.
        /// </value>
        public int RefreshTokenUsage { get; set; } = (int)TokenUsage.OneTimeOnly;

        /// <summary>
        /// Gets or sets a value indicating whether [update access token claims on refresh].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [update access token claims on refresh]; otherwise, <c>false</c>.
        /// </value>
        public bool UpdateAccessTokenClaimsOnRefresh { get; set; }

        /// <summary>
        /// Gets or sets the refresh token expiration.
        /// </summary>
        /// <value>
        /// The refresh token expiration.
        /// </value>
        public int RefreshTokenExpiration { get; set; } = (int)TokenExpiration.Absolute;

        /// <summary>
        /// Gets or sets the type of the access token.
        /// </summary>
        /// <value>
        /// The type of the access token.
        /// </value>
        public int AccessTokenType { get; set; } = (int)0; // AccessTokenType.Jwt;

        /// <summary>
        /// Gets or sets a value indicating whether [enable local login].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [enable local login]; otherwise, <c>false</c>.
        /// </value>
        public bool EnableLocalLogin { get; set; } = true;

        /// <summary>
        /// Gets or sets the identity provider restrictions.
        /// </summary>
        /// <value>
        /// The identity provider restrictions.
        /// </value>
        public List<ClientIdPRestriction> IdentityProviderRestrictions { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [include JWT identifier].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [include JWT identifier]; otherwise, <c>false</c>.
        /// </value>
        public bool IncludeJwtId { get; set; }

        /// <summary>
        /// Gets or sets the claims.
        /// </summary>
        /// <value>
        /// The claims.
        /// </value>
        public List<ClientClaim> Claims { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [always send client claims].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [always send client claims]; otherwise, <c>false</c>.
        /// </value>
        public bool AlwaysSendClientClaims { get; set; }

        /// <summary>
        /// Gets or sets the client claims prefix.
        /// </summary>
        /// <value>
        /// The client claims prefix.
        /// </value>
        public string ClientClaimsPrefix { get; set; } = "client_";

        /// <summary>
        /// Gets or sets the pair wise subject salt.
        /// </summary>
        /// <value>
        /// The pair wise subject salt.
        /// </value>
        public string PairWiseSubjectSalt { get; set; }

        /// <summary>
        /// Gets or sets the allowed cors origins.
        /// </summary>
        /// <value>
        /// The allowed cors origins.
        /// </value>
        public List<ClientCorsOrigin> AllowedCorsOrigins { get; set; }

        /// <summary>
        /// Gets or sets the properties.
        /// </summary>
        /// <value>
        /// The properties.
        /// </value>
        public List<ClientProperty> Properties { get; set; }

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
        /// Gets or sets the user sso lifetime.
        /// </summary>
        /// <value>
        /// The user sso lifetime.
        /// </value>
        public int? UserSsoLifetime { get; set; }

        /// <summary>
        /// Gets or sets the type of the user code.
        /// </summary>
        /// <value>
        /// The type of the user code.
        /// </value>
        public string UserCodeType { get; set; }

        /// <summary>
        /// Gets or sets the device code lifetime.
        /// </summary>
        /// <value>
        /// The device code lifetime.
        /// </value>
        public int DeviceCodeLifetime { get; set; } = 300;

        /// <summary>
        /// Gets or sets a value indicating whether [non editable].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [non editable]; otherwise, <c>false</c>.
        /// </value>
        public bool NonEditable { get; set; }
    }
}