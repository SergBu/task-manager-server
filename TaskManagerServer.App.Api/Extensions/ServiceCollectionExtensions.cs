using System.Reflection;
using System.Text.Json.Serialization;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.OpenApi.Models;
using TaskManagerServer.App.Api.Attributes;
using TaskManagerServer.App.Api.Authorization;
using TaskManagerServer.App.Api.Services;
using TaskManagerServer.Infra.Database;
using TaskManagerServer.Lib.App.Validation;
using TaskManagerServer.Lib.Core.Interfaces;

namespace TaskManagerServer.App.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFilters(this IServiceCollection services)
    {
        services.AddControllers(options =>
        {
            options.Filters.Add<ValidateModelFilterAttribute>();
            options.Filters.Add<AccessDeniedFilterAttribute>();
            options.Filters.Add<NotFoundEntityFilterAttribute>();
        }).AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });
        return services;
    }

    public static IServiceCollection AddValidation(this IServiceCollection services)
    {
        services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
        services.AddValidatorsFromAssemblyContaining<ValidateModelFilterAttribute>();
        services.AddScoped<IModelState>(provider =>
            new ModelState(provider.GetRequiredService<IActionContextAccessor>().ActionContext?.ModelState ??
                           new ModelStateDictionary()));

        return services;
    }

    public static IServiceCollection ConfigureRouting(this IServiceCollection services)
    {
        return services.Configure<RouteOptions>(options =>
        {
            options.LowercaseUrls = true;
            options.LowercaseQueryStrings = true;
        });
    }

    public static IServiceCollection AddAuthorization(this IServiceCollection services, IConfiguration configuration,
        IWebHostEnvironment environment)
    {
        services.AddSingleton<IAuthorizationMiddlewareResultHandler, AuthorizationMiddlewareResultHandler>();
        services.AddAuthorization();
        services.AddHttpContextAccessor();
        services.AddTransient<ICurrentUserService, CurrentUserService>();
        return services;
    }

    public static IServiceCollection AddSwaggerWithVersioning(this IServiceCollection services,
        IWebHostEnvironment environment)
    {
        services.AddApiVersioning(o =>
        {
            o.ReportApiVersions = true;
            o.AssumeDefaultVersionWhenUnspecified = true;
            o.DefaultApiVersion = new ApiVersion(1, 0);
        });

        services.AddVersionedApiExplorer(setup =>
        {
            setup.GroupNameFormat = "'v'VVV";
            setup.SubstituteApiVersionInUrl = true;
        });

        if (!environment.IsDevelopment() && !environment.IsStaging())
            return services;

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1",
                new OpenApiInfo
                { Title = $"TaskManager.{Assembly.GetExecutingAssembly().GetName().Name}", Version = "v1" });

            var directory = new DirectoryInfo(AppContext.BaseDirectory);
            foreach (var file in directory.GetFiles($"{Assembly.GetExecutingAssembly().GetName().Name}*.xml"))
            {
                c.CustomSchemaIds(t => t.FullName);
                c.IncludeXmlComments(file.FullName);
            }

            c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

            //c.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
            //{
            //    Type = SecuritySchemeType.Http,
            //    Scheme = "bearer"
            //});

            //c.AddSecurityRequirement(new OpenApiSecurityRequirement
            //{
            //    {
            //        new OpenApiSecurityScheme
            //        {
            //            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "bearer" }
            //        },
            //        []
            //    }
            //});
        });

        return services;
    }
}