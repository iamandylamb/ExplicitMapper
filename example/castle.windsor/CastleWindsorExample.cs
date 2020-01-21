using Microsoft.VisualStudio.TestTools.UnitTesting;
using Castle.MicroKernel.Registration;
using ExplicitMapper;
using ExplicitMapper.Example;

namespace Castle.Windsor.Example
{
    [TestClass]
    public class CastleWindsorExample
    {
        [TestMethod]
        public void Example()
        {
            var container = new WindsorContainer();

            // Install all mapper classes from an assembly.
            container.Install(new MapperInstaller(Classes.FromAssemblyContaining<UserRegistrationMapper>()));

            // Register specific generic mappers.
            container.Register(Component.For(typeof(XmlSerializerMapper<>))
                                        .ImplementedBy(typeof(XmlSerializerMapper<>))
                                        .LifestyleSingleton());

            Assert.IsNotNull(container.Resolve<IMapper<UserRegistrationModel, User>>());
        }
    }
}
