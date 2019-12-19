using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExplicitMapper.Test
{
    [TestClass]
    public class StructMapperTest
    {
        private class ConcreteStructMapper : StructMapper<ModelA, bool>
        {
            protected override void Map(ModelA source, out bool destination)
            {
                destination = source.BoolA;
            }
        }

        private IMapper<ModelA, bool> target = new ConcreteStructMapper();

        [TestMethod]
        public void MapsBoolean()
        {
            var expected = true;

            var actual = target.Map(new ModelA { BoolA = expected });

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void MapsBooleans()
        {
            var source = new[] { new ModelA { BoolA = true }, new ModelA { BoolA = false }, new ModelA { BoolA = true } };

            var expected = source.Select(x => x.BoolA).ToArray();

            var actual = target.Map(source).ToArray();

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task MapsBooleanAsync()
        {
            var expected = true;

            var actual = await target.MapAsync(new ModelA { BoolA = expected });

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task MapsBooleansAsync()
        {
            var source = new[] { new ModelA { BoolA = true }, new ModelA { BoolA = false }, new ModelA { BoolA = true } };

            var expected = source.Select(x => x.BoolA).ToArray();

            var actual = await target.MapAsync(source);

            CollectionAssert.AreEqual(expected, actual.ToArray());
        }
    }
}