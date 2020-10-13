using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Mix.Core;
using System;
using System.IO;

namespace Mix.Api
{
    /// <summary>
    /// Startup
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// 配置
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// 服务配置
        /// </summary>
        /// <param name="services"></param>
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // 配置验证器FluentValidtion
            services.AddMvc()
                .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Latest)
                .AddFluentValidation(fu =>
                {
                    fu.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                    fu.RegisterValidatorsFromAssemblyContaining<ValidableObject>();
                });

            // 配置Swagger
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("mixsystem", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "MixSystem API",
                    Description = "API for MixSystem",
                    Contact = new OpenApiContact()
                    {
                        Name = "zoulei",
                        Email = "None",
                        Url = new System.Uri("https://github.com/zoulei426/MixSystem")
                    }
                });

                // include document file
                option.IncludeXmlComments(
                    Path.Combine(AppContext.BaseDirectory,
                        $"{typeof(Startup).Assembly.GetName().Name}.xml")
                    , true);

                // 为 Swagger 添加 Bearer Token 认证
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Description = "Please enter into field the word 'Bearer' followed by a space and the JWT value",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference()
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        }, Array.Empty<string>()
                    }
                });
            });
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            // Enable middleware to serve swagger-ui (HTML, JS, CSS etc.), specifying the Swagger JSON endpoint
            app.UseSwaggerUI(option =>
            {
                option.SwaggerEndpoint("/swagger/mixsystem/swagger.json", "MixSystem Docs");
                option.RoutePrefix = string.Empty;
                option.DocumentTitle = "MixSystem API";
            });
        }
    }
}