using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Autofac;
using ExplicitMapper;
using Example.ExplicitMapper;

namespace Example.Autofac
{
    [TestClass]
    public class Example
    {
        [TestMethod]
        public void Test()
        {
            var builder = new ContainerBuilder();

            // Install all mapper classes from an assembly.
            builder.RegisterMappers(Assembly.GetAssembly(typeof(UserRegistrationMapper)))
                   // Register specific generic mappers.
                   .RegisterGeneric(typeof(XmlSerializerMapper<>)).AsSelf();

            var container = builder.Build();

            Assert.IsNotNull(container.Resolve<IMapper<UserRegistrationModel, User>>());
        }

        

    }
}
