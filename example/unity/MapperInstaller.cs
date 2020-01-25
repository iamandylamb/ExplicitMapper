using System.Linq;
using System.Reflection;
using Unity;
using ExplicitMapper.DependencyInjection;

namespace Example.Unity
{
    public static class MapperInstaller
    {
        public static IUnityContainer RegisterMappers(this IUnityContainer container, Assembly assembly)
        {
            return assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && !t.IsGenericTypeDefinition)
                .SelectMany(t => t.GetMappers()
                    .Select(i => new { Service = i, Implementation = t }))
                .Aggregate(container, (c, s) => 
                    c.RegisterSingleton(s.Service, s.Implementation)); // Other lifestyles may be preferable.
        }

    }
}