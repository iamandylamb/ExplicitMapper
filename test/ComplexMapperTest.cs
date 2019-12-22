using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExplicitMapper.Test
{
    [TestClass]
    public class ComplexMapperTest
    {
        private class ComplexChildMapper : StringMapper<ChildSourceModel>
        {
            protected override void Map(ChildSourceModel source, StringBuilder destination)
            {
                destination.Append(source.StringValue);
            }
        }

        private class ComplexMapper : ClassMapper<SourceModel, DestinationModel>
        {
            private IMapper<ChildSourceModel, string> childMapper;

            public ComplexMapper(IMapper<ChildSourceModel, string> childMapper)
            {
                this.childMapper = childMapper;
            }

            protected override void Map(SourceModel source, DestinationModel destination)
            {
                destination.StringValue = this.childMapper.Map(source.Child);

                destination.IntValue = source.IntValue;

                destination.BoolValue = source.BoolValue;
            }
        }

        private IMapper<SourceModel, DestinationModel> target = new ComplexMapper(new ComplexChildMapper());

        [TestMethod]
        public void MapsSingleClass()
        {
            var expected = new DestinationModel 
            { 
                StringValue = "ABC",
                BoolValue = true,
                IntValue = 1
            };

            var source = new SourceModel 
            {
                Child = new ChildSourceModel { StringValue = "ABC" },
                BoolValue = true,
                IntValue = 1
            };

            var actual = target.Map(source);

            var serializer = new XmlSerializerMapper<DestinationModel>();

            Assert.AreEqual(
                serializer.Map(expected), 
                serializer.Map(actual));
        }
    }
}