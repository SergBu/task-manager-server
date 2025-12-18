using MapsterMapper;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskManagerServer.Lib.App.Mapping;
using TaskManagerServer.Lib.App.Validation;
using TaskManagerServer.Lib.Core.Interfaces;
using TaskManagerServer.Lib.Core.TaskManagerServer;
using TaskManagerServer.Lib.App.Services;

namespace TaskManagerServer.Lib.App.Extensions;

public static class ServiceCollectionExtensions
{

    
    public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddSingleton(MappingConfig.ConfigureMapster());
        services.AddScoped<IMapper, ServiceMapper>();
        
        services.AddValidatorsFromAssemblyContaining<ValidatorsFactory>();
        services.AddScoped<IValidatorsFactory, ValidatorsFactory>();

        services.AddScoped<ITaskService, TaskService>();

        return services;
    }
}