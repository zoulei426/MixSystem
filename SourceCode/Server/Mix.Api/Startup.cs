using Autofac;
using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using IdentityServer4.AccessTokenValidation;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mix.Data;
using Mix.Data.Repositories;
using Mix.Library.Entities.Validators;
using Mix.Library.Repositories.Accounts;
using Mix.Service.Core.Extensions;
using Mix.Service.Core.Modules;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Mix.Api
{
    /// <summary>
    /// Startup
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 配置
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// 服务配置
        /// </summary>
        /// <param name="services"></param>
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // 注册缓存中间件
            services.AddHttpCacheHeaders(expires =>
            {
                expires.MaxAge = 60;
                expires.CacheLocation = CacheLocation.Private;
            }, validation =>
            {
                validation.MustRevalidate = true;
            });

            // 注册响应缓存
            services.AddResponseCaching();

            services.AddMvc(options =>
            {
                options.ReturnHttpNotAcceptable = true; // 不支持的类型将返回406
                options.CacheProfiles.Add("120sCacheProfile", new CacheProfile
                {
                    Duration = 120
                });
            })
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            })
            .AddXmlDataContractSerializerFormatters() // 添加xml格式
                .SetCompatibilityVersion(CompatibilityVersion.Latest)
                .AddFluentValidation(fv => // 配置FluentValidation
                {
                    fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                    fv.ImplicitlyValidateChildProperties = true;
                    fv.RegisterValidatorsFromAssemblyContaining<CompanyAddDtoValidator>();
                })
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.InvalidModelStateResponseFactory = context =>
                    {
                        var problemDetails = new ValidationProblemDetails(context.ModelState)
                        {
                            Type = "http://www.baidu.com",
                            Title = "Unprocessable Entity!",
                            Status = StatusCodes.Status422UnprocessableEntity,
                            Detail = "验证错误",
                            Instance = context.HttpContext.Request.Path
                        };

                        problemDetails.Extensions.Add("traceId", context.HttpContext.TraceIdentifier);

                        return new UnprocessableEntityObjectResult(problemDetails)
                        {
                            ContentTypes = { "application/problem+json" }
                        };
                    };
                });

            // 添加自定义MediaType
            services.Configure<MvcOptions>(config =>
            {
                var newtonsoftJsonOutputFormatter = config.OutputFormatters.OfType<NewtonsoftJsonOutputFormatter>()?.FirstOrDefault();
                newtonsoftJsonOutputFormatter?.SupportedMediaTypes.Add(
                    "application/vnd.mix.hateoas+json");
            });

            // 注册AutoMapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies().Where(r => r.FullName.Contains("Mix")).ToArray());

            // 注册IdentityServer4授权认证
            services.AddAuthorization();
            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = "http://localhost:5999";
                    options.RequireHttpsMetadata = false;
                    options.ApiName = "api1";
                    options.SaveToken = true;
                });

            // 注册国际化
            services.AddJsonLocalization(options => options.ResourcesPath = "Resources");

            services.AddFreeSql(Configuration);

            services.AddTransient<ICurrentUser, CurrentUser>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient(typeof(IAuditBaseRepository<>), typeof(AuditBaseRepository<>));
            services.AddTransient(typeof(IAuditBaseRepository<,>), typeof(AuditBaseRepository<,>));

            // 注册redis
            //services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect("localhost"));

            // 注册Swagger
            services.AddSwaggerGen();
        }

        /// <summary>
        /// AutoFact注册
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new RepositoryModule());
            builder.RegisterModule(new ServiceModule());
            builder.RegisterModule(new DependencyModule());
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            var supportedCultures = new List<CultureInfo>
            {
                new CultureInfo("en-US"),
                new CultureInfo("zh-CN"),
                new CultureInfo("fr-FR")
            };
            var options = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-US"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            };

            app.UseRequestLocalization(options);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                   {
                       context.Response.StatusCode = 500;
                       await context.Response.WriteAsync(":( \n Unexpected Error!");
                   });
                });
            }

            app.UseHttpsRedirection();

            //app.UseResponseCaching();

            app.UseHttpCacheHeaders();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            // Enable middleware to serve swagger-ui (HTML, JS, CSS etc.), specifying the Swagger JSON endpoint
            app.UseSwaggerUI(option =>
            {
                option.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                option.RoutePrefix = string.Empty;
                option.DocumentTitle = "MixSystem API";
            });
        }
    }
}