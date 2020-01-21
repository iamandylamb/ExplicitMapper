using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.DependencyInjection;
using ExplicitMapper;
using Example.ExplicitMapper;

namespace microsoft.dependencies
{
    [TestClass]
    public class MicrosoftDependenciesExample
    {
        [TestMethod]
        public void Example()
        {
            var serviceCollection = new ServiceCollection();

            var services = 
                // Install all mapper classes from an assembly.
                serviceCollection.AddMappers(Assembly.GetAssembly(typeof(UserRegistrationMapper)).GetTypes())
                                 // Register specific generic mappers.
                                 .AddSingleton(typeof(XmlSerializerMapper<>), typeof(XmlSerializerMapper<>))
                                 .BuildServiceProvider();

            Assert.IsNotNull(services.GetService<IMapper<UserRegistrationModel, User>>());
        }
    }
}
