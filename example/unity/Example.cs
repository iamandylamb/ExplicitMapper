using System.Reflection;
using System.Security.Cryptography;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity;
using ExplicitMapper;
using Example.ExplicitMapper;

namespace Example.Unity
{
    [TestClass]
    public class Example
    {
        [TestMethod]
        public void Test()
        {
            var container = new UnityContainer();

            // Install all mapper classes from an assembly.
            container.RegisterMappers(Assembly.GetAssembly(typeof(UserRegistrationMapper)))
                     // Register specific generic mappers.
                     .RegisterSingleton(typeof(XmlSerializerMapper<>), typeof(XmlSerializerMapper<>))
                    // Register other dependencies.
                    .RegisterSingleton<HashAlgorithm, SHA1CryptoServiceProvider>();

            Assert.IsNotNull(container.Resolve<IMapper<UserRegistrationModel, User>>());
        }

        

    }
}
