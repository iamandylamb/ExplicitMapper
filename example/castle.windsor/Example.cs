using System.Security.Cryptography;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using ExplicitMapper;
using Example.ExplicitMapper;

namespace Example.Castle.Windsor
{
    [TestClass]
    public class Example
    {
        [TestMethod]
        public void Test()
        {
            var container = new WindsorContainer();

            // Install all mapper classes from an assembly.
            container.Install(new MapperInstaller(Classes.FromAssemblyContaining<UserRegistrationMapper>()));

            // Register specific generic mappers.
            container.Register(Component.For(typeof(XmlSerializerMapper<>))
                                        .ImplementedBy(typeof(XmlSerializerMapper<>))
                                        .LifestyleSingleton());

            // Register other dependencies.
            container.Register(Component.For<HashAlgorithm>()
                                        .ImplementedBy<SHA1CryptoServiceProvider>()
                                        .LifestyleSingleton());

            Assert.IsNotNull(container.Resolve<IMapper<UserRegistrationModel, User>>());
        }
    }
}
