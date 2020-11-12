﻿using FreeSql;
using Mix.Library.Entities;
using Mix.Library.Entities.Databases.IdentityServer4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mix.Library.DbContexts.IdentityServer4
{
    /// <summary>
    /// DbContext for the IdentityServer configuration data.
    /// </summary>
    /// <seealso cref="FreeSql.DbContext" />
    /// <seealso cref="IdentityServer4.FreeSql.Interfaces.IConfigurationDbContext" />
    public class ConfigurationDbContext : ConfigurationDbContext<ConfigurationDbContext>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationDbContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="storeOptions">The store options.</param>
        /// <exception cref="ArgumentNullException">storeOptions</exception>
        public ConfigurationDbContext(IFreeSql<ConfigurationDbContext> freeSql, ConfigurationStoreOptions storeOptions)
            : base(freeSql, storeOptions)
        {
        }
    }

    /// <summary>
    /// DbContext for the IdentityServer configuration data.
    /// </summary>
    /// <seealso cref="Free.DbContext" />
    /// <seealso cref="IdentityServer4.Free.Interfaces.IConfigurationDbContext" />
    public class ConfigurationDbContext<TContext> : DbContext, IConfigurationDbContext
        where TContext : DbContext, IConfigurationDbContext
    {
        private readonly IFreeSql<ConfigurationDbContext> freeSql;

        //private readonly DbContextOptions options;
        private readonly ConfigurationStoreOptions storeOptions;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationDbContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="storeOptions">The store options.</param>
        /// <exception cref="ArgumentNullException">storeOptions</exception>
        public ConfigurationDbContext(IFreeSql<ConfigurationDbContext> freeSql, ConfigurationStoreOptions storeOptions)
            : base(freeSql, null)
        {
            this.freeSql = freeSql;
            this.storeOptions = storeOptions ?? throw new ArgumentNullException(nameof(storeOptions));
        }

        /// <summary>
        /// Gets or sets the clients.
        /// </summary>
        /// <value>
        /// The clients.
        /// </value>
        public DbSet<Client> Clients { get; set; }

        /// <summary>
        /// Gets or sets the identity resources.
        /// </summary>
        /// <value>
        /// The identity resources.
        /// </value>
        public DbSet<IdentityResource> IdentityResources { get; set; }

        /// <summary>
        /// Gets or sets the API resources.
        /// </summary>
        /// <value>
        /// The API resources.
        /// </value>
        public DbSet<ApiResource> ApiResources { get; set; }

        /// <summary>
        /// Saves the changes.
        /// </summary>
        /// <returns></returns>
        public override async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

        /// <summary>
        /// Override this method to further configure the model that was discovered by convention from the entity types
        /// exposed in <see cref="T:FreeSql.DbSet`1" /> properties on your derived context. The resulting model may be cached
        /// and re-used for subsequent instances of your derived context.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context. Databases (and other extensions) typically
        /// define extension methods on this object that allow you to configure aspects of the model that are specific
        /// to a given database.</param>
        /// <remarks>
        /// If a model is explicitly set on the options for this context (via <see cref="M:FreeSql.DbContextOptionsBuilder.UseModel(FreeSql.Metadata.IModel)" />)
        /// then this method will not be run.
        /// </remarks>
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.ConfigureClientContext(storeOptions);
        //    modelBuilder.ConfigureResourcesContext(storeOptions);

        //    base.OnModelCreating(modelBuilder);
        //}

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseFreeSql(orm: freeSql);
            //builder.UseOptions(options: options);
            base.OnConfiguring(builder);
        }
    }
}