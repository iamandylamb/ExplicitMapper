using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExplicitMapper 
{
    public abstract class Mapper<TSource, TDestination> : IMapper<TSource, TDestination>
    {
        public abstract TDestination Map(TSource source);

        public IEnumerable<TDestination> Map(IEnumerable<TSource> source) => 
            source.Select(Map);

        public async Task<TDestination> MapAsync(TSource source) => 
            await Task.Run(() => Map(source)).ConfigureAwait(false);

        public async Task<IEnumerable<TDestination>> MapAsync(IEnumerable<TSource> source) => 
            await Task.Run(() => Map(source)).ConfigureAwait(false); // Sequentially map all source items, asynchronously.

        public async Task<IEnumerable<TDestination>> MapParallel(IEnumerable<TSource> source) => 
            await Task.WhenAll(source.Select(MapAsync)).ConfigureAwait(false); // Concurrently map all source item, asynchronously.
    }

}