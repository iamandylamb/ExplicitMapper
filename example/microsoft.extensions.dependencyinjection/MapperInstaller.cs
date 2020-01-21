using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using ExplicitMapper;

namespace Example.Microsoft.Extensions.DependencyInjection
{
    public static class MapperInstaller
    {
        public static IServiceCollection AddMappers(this IServiceCollection serviceCollection, IEnumerable<Type> classes)
        {
            return classes
                .Where(t => t.IsClass && !t.IsAbstract && !t.IsGenericTypeDefinition)
                .SelectMany(t => t.GetInterfaces().Where(IsMapper)
                    .Select(i => new { Service = i, Implementation = t }))
                .Aggregate(serviceCollection, (sc, s) => 
                    sc.AddSingleton(s.Service, s.Implementation)); // Other lifestyles may be preferable.
        }

        private static bool IsMapper(Type service)
        {
            return service.IsGenericType
                && service.GetGenericTypeDefinition() == typeof(IMapper<,>);
        }
    }
}