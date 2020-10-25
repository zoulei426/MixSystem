using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.OpenApi.Models;
using Mix.Library.Entities.Databases;
using Mix.Service.Core;
using Serilog;
using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Mix.Api
{
    /// <summary>
    /// Swagger扩展
    /// </summary>
    public static class SwaggerExtensions
    {
        /// <summary>
        /// Adds the swagger gen.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns></returns>
        public static IServiceCollection AddSwaggerGen(this IServiceCollection services)
        {
            //Swagger重写PascalCase，改成SnakeCase模式
            services.TryAddEnumerable(ServiceDescriptor.Transient<IApiDescriptionProvider, ApiDescriptionProvider>());

            //Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(options =>
            {
                string ApiName = "Mix.Api";
                options.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = ApiName + RuntimeInformation.FrameworkDescription,
                    Version = "v1",
                    Contact = new OpenApiContact
                    {
                        Name = ApiName,
                        Email = "1815207163@qq.com",
                        Url = new System.Uri("https://github.com/zoulei426/MixSystem")
                    },
                    //License = new OpenApiLicense
                    //{
                    //    Name = ApiName + " 官方文档",
                    //    Url = new Uri("https://luoyunchong.github.io/vovo-docs/dotnetcore/lin-cms/dotnetcore-start.html")
                    //}
                });

                //添加一个必须的全局安全信息，和AddSecurityDefinition方法指定的方案名称要一致，这里是Bearer。
                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference()
                            {
                                Id =  "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        Array.Empty<string>()
                    }
                });
                options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                {
                    Name = "Authorization", //jwt默认的参数名称
                    Description = "JWT授权(数据将在请求头中进行传输) 参数结构: \"Authorization: Bearer {token}\"",
                    //Description = "Please enter into field the word 'Bearer' followed by a space and the JWT value",
                    In = ParameterLocation.Header, //jwt默认存放Authorization信息的位置(请求头中)
                    Type = SecuritySchemeType.ApiKey
                });

                //options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                //{
                //    {
                //        new OpenApiSecurityScheme
                //        {
                //            Reference = new OpenApiReference()
                //            {
                //                Id =  "oauth2",
                //                Type = ReferenceType.SecurityScheme
                //            }
                //        },
                //        Array.Empty<string>()
                //    }
                //});
                // Define the OAuth2.0 scheme that's in use (i.e. Implicit Flow)
                //options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                //{
                //    Type = SecuritySchemeType.OAuth2,
                //    Flows = new OpenApiOAuthFlows
                //    {
                //        AuthorizationCode = new OpenApiOAuthFlow
                //        {
                //            AuthorizationUrl = new Uri("https://localhost:5003/connect/authorize", UriKind.Absolute),
                //            TokenUrl = new Uri("https://localhost:5003/connect/token", UriKind.Absolute),
                //            Scopes = new Dictionary<string, string>
                //            {
                //                { "LinCms.Web", "Access read/write LinCms.Web" }
                //            }
                //        },
                //        Password = new OpenApiOAuthFlow()
                //        {
                //            AuthorizationUrl = new Uri("https://localhost:5003/connect/authorize", UriKind.Absolute),
                //            TokenUrl = new Uri("https://localhost:5003/connect/token", UriKind.Absolute),
                //            Scopes = new Dictionary<string, string>
                //            {
                //                { "openid", "Access read openid" },
                //                { "offline_access", "Access read offline_access" },
                //                { "LinCms.Web", "Access read/write LinCms.Web" }
                //            }
                //        }
                //    }
                //});

                try
                {
                    string xmlPath = Path.Combine(AppContext.BaseDirectory, $"{typeof(Startup).Assembly.GetName().Name}.xml");
                    options.IncludeXmlComments(xmlPath, true);
                    //实体层的xml文件名
                    string xmlEntityPath = Path.Combine(AppContext.BaseDirectory, $"{typeof(Company).Assembly.GetName().Name}.xml");
                    options.IncludeXmlComments(xmlEntityPath);
                    //Dto所在类库
                    //string applicationPath = Path.Combine(AppContext.BaseDirectory, $"{typeof(IApplicationService).Assembly.GetName().Name}.xml");
                    //options.IncludeXmlComments(applicationPath);
                }
                catch (Exception ex)
                {
                    Log.Logger.Warning(ex.Message);
                }

                //options.AddServer(new OpenApiServer()
                //{
                //    Url = "https://localhost:5002",
                //    Description = "本地"
                //});
                //options.AddServer(new OpenApiServer()
                //{
                //    Url = "https://api.igeekfan.cn",
                //    Description = "服务端"
                //});

                options.CustomOperationIds(apiDesc =>
                {
                    var controllerAction = apiDesc.ActionDescriptor as ControllerActionDescriptor;
                    return $"{controllerAction.ControllerName}-{controllerAction.ActionName}-{ controllerAction.GetHashCode()}";
                });
            });
            return services;
        }
    }
}