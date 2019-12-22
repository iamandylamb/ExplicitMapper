using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ExplicitMapper 
{
    /// <summary>
    /// XML serializer mapper. Maps an object to an XML string.
    /// </summary>
    public sealed class XmlSerializerMapper<TSource> : StringMapper<TSource>
    {
        private static XmlSerializer serializer = new XmlSerializer(typeof(TSource)) ;

        protected override void Map(TSource source, StringBuilder destination)
        {
            using(StringWriter textWriter = new StringWriter()) 
            {
                using(XmlWriter xmlWriter = XmlWriter.Create(textWriter)) 
                {
                    serializer.Serialize(xmlWriter, source);
                }

                destination.Append(textWriter);
            }
        }
    }

}