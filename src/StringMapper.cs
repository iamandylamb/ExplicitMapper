using System.Text;

namespace ExplicitMapper 
{
    /// <summary>
    /// Base mapper class for 'string' destinations.
    /// </summary>
    public abstract class StringMapper<TSource> : Mapper<TSource, string>
    {
        public override sealed string Map(TSource source)
        {
            var destination = new StringBuilder();

            Map(source, destination);

            return destination.ToString();
        }

        protected abstract void Map(TSource source, StringBuilder destination);
    }

}