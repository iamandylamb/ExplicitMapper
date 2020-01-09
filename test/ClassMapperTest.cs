using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExplicitMapper.Test
{
    [TestClass]
    public class ClassMapperTest
    {
        private class Model
        {
            public int Value { get; set; }
        }

        private class Response
        {
            public int Value { get; set; }
        }
        
        private class DirectMapper : ClassMapper<Model, Response>
        {
            protected override void Map(Model source, Response destination)
            {
                destination.Value = source.Value;
            }
        }

        private IMapper<Model, Response> target = new DirectMapper();

        [TestMethod]
        public void MapsSingleClass()
        {
            var expected = new Response { Value = 1 };

            var actual = target.Map(new Model { Value = 1 });

            Assert.AreEqual(expected.Value, actual.Value);
        }

        [TestMethod]
        public void MapsMultipleValues()
        {
            var source = new[] { new Model { Value = 1 }, new Model { Value = 2 }, new Model { Value = 3 } };

            var expected = new[] { 1, 2, 3 }; // Just checking values.

            var actual = target.Map(source).Select(x => x.Value).ToArray();

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task MapsSingleValueAsync()
        {
            var expected = new Response { Value = 1 };

            var actual = await target.MapAsync(new Model { Value = 1 });

            Assert.AreEqual(expected.Value, actual.Value);
        }

        [TestMethod]
        public async Task MapsMultipleValuesAsync()
        {
            var source = new[] { new Model { Value = 1 }, new Model { Value = 2 }, new Model { Value = 3 } };

            var expected = new[] { 1, 2, 3 }; // Just checking values.

            var actual = await target.MapAsync(source);

            CollectionAssert.AreEqual(expected, actual.Select(x => x.Value).ToArray());
        }

        [TestMethod]
        public async Task MapsMultipleValuesParallel()
        {
            var source = new[] { new Model { Value = 1 }, new Model { Value = 2 }, new Model { Value = 3 } };

            var expected = new[] { 1, 2, 3 }; // Just checking values.

            var actual = await target.MapParallel(source);

            CollectionAssert.AreEqual(expected, actual.Select(x => x.Value).ToArray());
        }
    }
}