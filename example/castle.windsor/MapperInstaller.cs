using System.Linq;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using ExplicitMapper.DependencyInjection;

namespace Example.Castle.Windsor
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
            container.Register(this.classes.Where(type => !type.IsGenericTypeDefinition && type.GetMappers().Any())
                                           .WithService.Select((type, _) => type.GetMappers())
                                           .LifestyleSingleton()); // Other lifestyles may be preferable.
        }
    }
}