using System.Reflection;
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
            var container = new Container(new MapperRegistry(Assembly.GetAssembly(typeof(UserRegistrationMapper))));

            Assert.IsNotNull(container.GetInstance<IMapper<UserRegistrationModel, User>>());
        }
    }
}
