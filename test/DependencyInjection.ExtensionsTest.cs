using System;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExplicitMapper.DependencyInjection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExplicitMapper.Test
{
    [TestClass]
    public class DependencyInjectionExtensionsTest
    {
        private class NonMapper
        {   
            public string Value { get; set; }
        }

        private class DerivedMapper : StringMapper<NonMapper>
        {
            protected override void Map(NonMapper source, StringBuilder destination)
            {
                destination.Append(source.Value);
            }
        }

        private class DerivedMapperWithOtherInterface : StructMapper<NonMapper, int>, IDisposable
        {
            public void Dispose()
            {
                throw new NotImplementedException();
            }

            protected override void Map(NonMapper source, out int destination)
            {
                destination = source.Value.Length;
            }
        }

        private class DerivedMapperWithOtherGenericInterface : ClassMapper<NonMapper, NonMapper>, IEquatable<DerivedMapperWithOtherGenericInterface>
        {
            public bool Equals(DerivedMapperWithOtherGenericInterface other)
            {
                throw new NotImplementedException();
            }

            protected override void Map(NonMapper source, NonMapper destination)
            {
                destination.Value = source.Value.ToUpper();
            }
        }

        private class MutlipleMappersInOne : StructMapper<NonMapper, double>, IMapper<NonMapper, decimal>
        {
            protected override void Map(NonMapper source, out double destination)
            {
                throw new NotImplementedException();
            }

            decimal IMapper<NonMapper, decimal>.Map(NonMapper source)
            {
                throw new NotImplementedException();
            }

            IEnumerable<decimal> IMapper<NonMapper, decimal>.Map(IEnumerable<NonMapper> source)
            {
                throw new NotImplementedException();
            }

            Task<decimal> IMapper<NonMapper, decimal>.MapAsync(NonMapper source)
            {
                throw new NotImplementedException();
            }

            Task<IEnumerable<decimal>> IMapper<NonMapper, decimal>.MapAsync(IEnumerable<NonMapper> source)
            {
                throw new NotImplementedException();
            }

            Task<IEnumerable<decimal>> IMapper<NonMapper, decimal>.MapParallel(IEnumerable<NonMapper> source)
            {
                throw new NotImplementedException();
            }
        }

        [TestMethod]
        public void NullReturnsEmptyCollection()
        {
            var actual = Extensions.GetMappers(null);

            Assert.IsFalse(actual.Any());
        }

        [TestMethod]
        public void NonMapperReturnsEmptyCollection()
        {
            var actual = typeof(NonMapper).GetMappers();

            Assert.IsFalse(actual.Any());
        }

        [TestMethod]
        public void OtherInterfacesAreIgnored()
        {
            var expected = new[] { typeof(IMapper<NonMapper, int>) };

            var actual = typeof(DerivedMapperWithOtherInterface).GetMappers().ToArray();

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void OtherGenericInterfacesAreIgnored()
        {
            var expected = new[] { typeof(IMapper<NonMapper, NonMapper>) };

            var actual = typeof(DerivedMapperWithOtherGenericInterface).GetMappers().ToArray();

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void MapperInterfaceReturned()
        {
            var expected = new[] { typeof(IMapper<NonMapper, string>) };

            var actual = typeof(DerivedMapper).GetMappers().ToArray();

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AllMapperInterfacesReturned()
        {
            var expected = new[] { typeof(IMapper<NonMapper, double>), typeof(IMapper<NonMapper, decimal>) };

            var actual = typeof(MutlipleMappersInOne).GetMappers().ToArray();

            CollectionAssert.AreEqual(expected, actual);
        }
    }
}