using System.Reflection;
using System.Security.Cryptography;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructureMap;
using ExplicitMapper;
using Example.ExplicitMapper;

namespace Example.StructureMap
{
    [TestClass]
    public class Example
    {
        [TestMethod]
        public void Test()
        {
            var container = new Container(_ => 
            {
                // Install all mapper classes from an assembly.
                _.IncludeRegistry(new MapperRegistry(Assembly.GetAssembly(typeof(UserRegistrationMapper))));
                // Register other dependencies.
                _.For<HashAlgorithm>().Use<SHA1CryptoServiceProvider>().Singleton();
            });

            Assert.IsNotNull(container.GetInstance<IMapper<UserRegistrationModel, User>>());
        }
    }
}
