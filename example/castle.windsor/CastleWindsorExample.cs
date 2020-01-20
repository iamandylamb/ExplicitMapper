using Microsoft.VisualStudio.TestTools.UnitTesting;
using Castle.Windsor;
using Castle.MicroKernel;
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

            // Install all 'root' mapper classes from an assembly.
            container.Install(new MapperInstaller(Classes.FromAssemblyContaining<UserRegistrationMapper>()));

            // Install specific mapper classes from any location.
            //container.Install(new MapperInstaller(Classes.From(typeof(Mapper3))));

            // Register specific generic mappers.
            // container.Register(Component.For(typeof(XmlSerializerMapper<>))
            //                             .ImplementedBy(typeof(XmlSerializerMapper<>))
            //                             .LifestyleSingleton());

            Assert.IsNotNull(container.Resolve<UserRegistrationMapper>());
        }
    }
}