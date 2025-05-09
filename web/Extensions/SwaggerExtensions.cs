using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.OpenApi.Models;

namespace Morn_Agi.Extensions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddCustomizedSwagger(this IServiceCollection services, IWebHostEnvironment env)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "API Auth Demo",
                    Description = "A simple demo with JWT Auth APIs and Basic Auth APIs",
                    Contact = new OpenApiContact
                    {
                        Name = @"GitHub Repository",
                        Email = string.Empty,
                        Url = new Uri("https://github.com/dotnet-labs/ApiAuthDemo")
                    }
                });

                var xmlPath = Path.Combine(Directory.GetCurrentDirectory(), "swagger-api.xml");
                c.IncludeXmlComments(xmlPath, true);

                // add JWT Authentication
                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "JWT Authentication",
                    Description = "Enter JWT Bearer token **_only_**",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer", // must be lower case
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };
                c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {securityScheme, new string[] { }}
                });

                // add Basic Authentication
                var basicSecurityScheme = new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "basic",
                    Reference = new OpenApiReference { Id = "BasicAuth", Type = ReferenceType.SecurityScheme }
                };
                c.AddSecurityDefinition(basicSecurityScheme.Reference.Id, basicSecurityScheme);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {basicSecurityScheme, new string[] { }}
                });

                var openIdSecurityScheme = new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OpenIdConnect,
                    Reference = new OpenApiReference { Id = "OpenIdConnectAuth", Type = ReferenceType.SecurityScheme },
                    OpenIdConnectUrl = new Uri("https://login.microsoftonline.com/organizations/v2.0/.well-known/openid-configuration")
                };
                c.AddSecurityDefinition(openIdSecurityScheme.Reference.Id, openIdSecurityScheme);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {openIdSecurityScheme, new string[] { }}
                });
            });

            return services;
        }

        public static IApplicationBuilder UseCustomizedSwagger(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger(c =>
            {
                //c.RouteTemplate = "swagger/v1/swagger.json";
                //c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
                //{
                //    swaggerDoc.Servers = new List<OpenApiServer> { new OpenApiServer { Url = $"{httpReq.Scheme}://{httpReq.Host.Value}/" } };
                //});

                //var basepath = "/api/AppStatus";
                //c.PreSerializeFilters.Add((swaggerDoc, httpReq) => swaggerDoc.BasePath = basepath);

                //c.PreSerializeFilters.Add((swaggerDoc, httpReq) => {
                //    IDictionary<string, PathItem> paths = new Dictionary<string, PathItem>();
                //    foreach (var path in swaggerDoc.Paths)
                //    {
                //        paths.Add(path.Key.Replace(basepath, "/"), path.Value);
                //    }
                //    swaggerDoc.Paths = paths;
                //});
            });
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                    c.DocumentTitle = "API Auth Demo";
                    c.DefaultModelsExpandDepth(0);
                    c.RoutePrefix = string.Empty;
                });
                //c.SwaggerEndpoint("/swagger/v1/swagger.json", "ASPNET Core DDD Project API v1.0");
                //c.SwaggerEndpoint("/swagger/v2/swagger.json", "ASPNET Core DDD Project API v2.0");

                // Show V1 first in Swagger
                // foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
                // {
                //     c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                //         description.GroupName.ToUpperInvariant());
                // }

                // Show V2 first in Swagger
                // foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions.Reverse())
                // {
                //     options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                //         description.GroupName.ToUpperInvariant());
                // }
            });

            return app;
        }

    }
}
