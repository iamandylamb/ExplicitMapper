namespace ExplicitMapper 
{
    /// <summary>
    /// Base mapper class for 'reference' destinations.
    /// </summary>
    public abstract class ClassMapper<TSource, TDestination> : Mapper<TSource, TDestination> 
        where TDestination: new()
    {
        public override sealed TDestination Map(TSource source)
        {
            TDestination destination = DestinationFactory<TDestination>.CreateInstance();

            Map(source, destination);

            return destination;
        }

        protected abstract void Map(TSource source, TDestination destination);
    }

}