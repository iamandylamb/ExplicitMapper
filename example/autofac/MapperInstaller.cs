using System.Linq;
using System.Reflection;
using Autofac;
using ExplicitMapper.DependencyInjection;

namespace Example.Autofac
{
    public static class MapperInstaller
    {
        public static ContainerBuilder RegisterMappers(this ContainerBuilder builder, Assembly assembly)
        {
            builder.RegisterAssemblyTypes(assembly)
                   .Where(type => !type.IsGenericTypeDefinition && type.GetMappers().Any())
                   .As(type => type.GetMappers())
                   .SingleInstance(); // Other lifestyles may be preferable.

            return builder;
        }
    }
}