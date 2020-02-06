using System.Reflection;
using System.Security.Cryptography;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.DependencyInjection;
using ExplicitMapper;
using Example.ExplicitMapper;

namespace Example.Microsoft.Extensions.DependencyInjection
{
    [TestClass]
    public class Example
    {
        [TestMethod]
        public void Test()
        {
            var serviceCollection = new ServiceCollection();

            var services = 
                // Install all mapper classes from an assembly.
                serviceCollection.AddMappers(Assembly.GetAssembly(typeof(UserRegistrationMapper)))
                                 // Register specific generic mappers.
                                 .AddSingleton(typeof(XmlSerializerMapper<>), typeof(XmlSerializerMapper<>))
                                 // Register other dependencies.
                                 .AddSingleton<HashAlgorithm, SHA1CryptoServiceProvider>()
                                 .BuildServiceProvider();

            Assert.IsNotNull(services.GetService<IMapper<UserRegistrationModel, User>>());
        }
    }
}
