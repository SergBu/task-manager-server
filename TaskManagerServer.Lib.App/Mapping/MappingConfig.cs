using System.Reflection;
using Mapster;

namespace TaskManagerServer.Lib.App.Mapping;

public static class MappingConfig
{
    public static TypeAdapterConfig ConfigureMapster()
    {
        TypeAdapterConfig.GlobalSettings.Default.Settings.MapEnumByName = true;
        var typeAdapterConfig = new TypeAdapterConfig
        {
            RequireExplicitMapping = false,
            AllowImplicitDestinationInheritance = true,
            Default = { Settings = { MapEnumByName = true}}
        };

        typeAdapterConfig.Apply(typeAdapterConfig.Scan(Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly()));
        
        typeAdapterConfig.Apply(new TaskManagerServerEntityMappingProfile());

        return typeAdapterConfig;
    }
}