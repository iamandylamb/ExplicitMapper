using System;
using System.Collections.Generic;
using System.Linq;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using ExplicitMapper;

namespace Castle.Windsor.Example
{
    public class MapperInstaller : IWindsorInstaller
    {
        private readonly FromDescriptor classes;

        public MapperInstaller(FromDescriptor classes)
        {
            this.classes = classes;
        }

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(this.classes.Where(type => !type.IsGenericTypeDefinition && GetMapper(type).Any())
                                           .WithService.Select((type, _) => GetMapper(type))
                                           .LifestyleSingleton()); // Other lifestyles may be preferable.
        }

        private static IEnumerable<Type> GetMapper(Type type)
        {
            return type.GetInterfaces().Where(i => i.IsGenericType
                                                && i.GetGenericTypeDefinition() == typeof(IMapper<,>));
        }
    }
}