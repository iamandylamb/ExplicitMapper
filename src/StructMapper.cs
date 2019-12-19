namespace ExplicitMapper 
{
    /// <summary>
    /// Base mapper class for 'struct' destinations.
    /// </summary>
    public abstract class StructMapper<TSource, TDestination> : Mapper<TSource, TDestination>
        where TDestination: struct
    {
        public override sealed TDestination Map(TSource source)
        {
            Map(source, out TDestination destination);

            return destination;
        }

        protected abstract void Map(TSource source, out TDestination destination);
    }

}