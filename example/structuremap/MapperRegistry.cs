using System.Reflection;
using StructureMap;
using ExplicitMapper;

namespace Example.StructureMap
{
    public class MapperRegistry : Registry
    {
        public MapperRegistry(Assembly assembly)
        {
            Scan(scanner => 
            {
                // Install all mapper classes from an assembly.
                scanner.Assembly(assembly);
                scanner.Exclude(type => type.IsGenericTypeDefinition);
                scanner.ConnectImplementationsToTypesClosing(typeof(IMapper<,>))
                       .OnAddedPluginTypes(config => config.Singleton()); // Other lifestyles may be preferable.
            });

            // Register specific generic mappers.
            this.ForSingletonOf(typeof(XmlSerializerMapper<>)).Use(typeof(XmlSerializerMapper<>)).Singleton();
        }
    }
}