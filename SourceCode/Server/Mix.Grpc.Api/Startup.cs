using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mix.Data;
using Mix.Data.Repositories;
using Mix.Grpc.Api.Services;
using Mix.Library.Repository.Accounts;
using Mix.Service.Core;
using Mix.Service.Core.Modules;

namespace Mix.Grpc.Api
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGrpc();

            services.AddFreeSql(configuration);

            // ×¢²áredis
            //services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect("localhost"));

            services.AddTransient<ICurrentUser, CurrentUser>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient(typeof(IAuditBaseRepository<>), typeof(AuditBaseRepository<>));
            services.AddTransient(typeof(IAuditBaseRepository<,>), typeof(AuditBaseRepository<,>));
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new RepositoryModule());
            builder.RegisterModule(new ServiceModule());
            builder.RegisterModule(new DependencyModule());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<AccountService>();
            });
        }
    }
}