using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace API.Configuration;

public static class SwaggerConfiguration
{
    public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddVersionedApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
        });

        services.AddApiVersioning(c =>
        {
            c.ReportApiVersions = true;

            c.AssumeDefaultVersionWhenUnspecified = true;
            c.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
        });

        services.AddSwaggerGen(c =>
        {
            c.DocInclusionPredicate((docName, apiDesc) => apiDesc.HttpMethod != null);
            c.EnableAnnotations();
            using (var serviceProvider = services.BuildServiceProvider())
            {
                var provider = serviceProvider.GetRequiredService<IApiVersionDescriptionProvider>();
                string? assemblyDescription = typeof(Program).Assembly.GetCustomAttribute<AssemblyDescriptionAttribute>()?.Description;
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    c.SwaggerDoc(description.GroupName, new OpenApiInfo()
                    {
                        Title = $"{typeof(Program).Assembly?.GetCustomAttribute<AssemblyProductAttribute>()?.Product} {ApiVersion.Get()}",
                        Version = ApiVersion.Get(),
                        Description = description.IsDeprecated ? $"{assemblyDescription} - DEPRECATED" : $"{assemblyDescription}"
                    });
                }
            }
        });



        return services;
    }

    public static IApplicationBuilder UseSwaggerConfiguration(this IApplicationBuilder app, IWebHostEnvironment env, string baseUrl, string serviceName)
    {
        if (env != null && env.IsProduction())
            return app;

        app.UseSwagger(c =>
        {
            c.RouteTemplate = baseUrl + "/swagger/{documentName}/swagger.json";
        });

        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint($"/{baseUrl}/swagger/v1/swagger.json", serviceName);
            c.RoutePrefix = $"{baseUrl}/swagger";
        });

        return app;

    }
}
