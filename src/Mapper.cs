using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExplicitMapper 
{
    public abstract class Mapper<TSource, TDestination> : IMapper<TSource, TDestination>
    {
        public abstract TDestination Map(TSource source);

        public IEnumerable<TDestination> Map(IEnumerable<TSource> source) => source.Select(Map);

        public async Task<TDestination> MapAsync(TSource source) => await Task.Run(() => Map(source));

        public async Task<IEnumerable<TDestination>> MapAsync(IEnumerable<TSource> source) => await Task.Run(() => Map(source)); // Single task for the whole collection.

        public async Task<IEnumerable<TDestination>> MapAsync_PerSource(IEnumerable<TSource> source) => await Task.WhenAll(source.Select(MapAsync)); // Task per source item.
    }

}