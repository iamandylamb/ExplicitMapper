using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExplicitMapper.Test
{
    [TestClass]
    public class StructMapperTest
    {
        private class Model
        {
            public int Value { get; set; }
        }
        
        private class PlusOneMapper : StructMapper<Model, int>
        {
            protected override void Map(Model source, out int destination)
            {
                destination = source.Value + 1;
            }
        }

        private IMapper<Model, int> target = new PlusOneMapper();

        [TestMethod]
        public void MapsSingleValue()
        {
            var expected = 2;

            var actual = target.Map(new Model { Value = 1 });

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void MapsMultipleValues()
        {
            var source = new[] { new Model { Value = 1 }, new Model { Value = 2 }, new Model { Value = 3 } };

            var expected = new[] { 2, 3, 4 };

            var actual = target.Map(source).ToArray();

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task MapsSingleValueAsync()
        {
            var expected = 2;

            var actual = await target.MapAsync(new Model { Value = 1 });

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task MapsMultipleValuesAsync()
        {
            var source = new[] { new Model { Value = 1 }, new Model { Value = 2 }, new Model { Value = 3 } };

            var expected = new[] { 2, 3, 4 };

            var actual = await target.MapAsync(source);

            CollectionAssert.AreEqual(expected, actual.ToArray());
        }

        [TestMethod]
        public async Task MapsMultipleValuesParallel()
        {
            var source = new[] { new Model { Value = 1 }, new Model { Value = 2 }, new Model { Value = 3 } };

            var expected = new[] { 2, 3, 4 };

            var actual = await target.MapParallel(source);

            CollectionAssert.AreEqual(expected, actual.ToArray());
        }
    }
}