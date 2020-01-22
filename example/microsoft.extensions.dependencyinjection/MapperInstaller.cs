using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using ExplicitMapper.DependencyInjection;

namespace Example.Microsoft.Extensions.DependencyInjection
{
    public static class MapperInstaller
    {
        public static IServiceCollection AddMappers(this IServiceCollection serviceCollection, Assembly assembly)
        {
            return assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && !t.IsGenericTypeDefinition)
                .SelectMany(t => t.GetMappers()
                    .Select(i => new { Service = i, Implementation = t }))
                .Aggregate(serviceCollection, (sc, s) => 
                    sc.AddSingleton(s.Service, s.Implementation)); // Other lifestyles may be preferable.
        }
    }
}