using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using ExplicitMapper;

namespace Example.Autofac
{
    public static class MapperInstaller
    {
        public static ContainerBuilder RegisterMappers(this ContainerBuilder builder, Assembly assembly)
        {
            builder.RegisterAssemblyTypes(assembly)
                   .Where(type => !type.IsGenericTypeDefinition && GetMapper(type).Any())
                   .As(GetMapper);

            return builder;
        }

        private static IEnumerable<Type> GetMapper(Type type)
        {
            return type.GetInterfaces().Where(i => i.IsGenericType
                                                && i.GetGenericTypeDefinition() == typeof(IMapper<,>));
        }
    }
}