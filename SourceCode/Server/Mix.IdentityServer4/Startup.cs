using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Mix.IdentityServer4
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var builder = services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;

                // see https://identityserver4.readthedocs.io/en/latest/topics/resources.html
                //options.EmitStaticAudienceClaim = true;
            });

            builder.AddTestUsers(TestUsers.Users);

            // not recommended for production - you need to store your key material somewhere secure
            builder.AddDeveloperSigningCredential();

            // in-memory, code config
            //builder.AddInMemoryIdentityResources(Config.GetIdentityResources());
            //builder.AddInMemoryApiResources(Config.GetApiResources());
            builder.AddInMemoryApiScopes(Config.GetApiScopes());
            builder.AddInMemoryClients(Config.GetClients());

            //builder.AddResourceOwnerValidator<ResourceOwnerPasswordValidator>();
            //builder.AddProfileService<ProfileService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseIdentityServer();

            app.UseEndpoints(endpoints =>
            {
            });
        }
    }
}