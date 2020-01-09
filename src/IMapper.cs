using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExplicitMapper 
{
    public interface IMapper<TSource, TDestination>
    {
        /// <summary>
        /// Map a source object to a desination object.
        /// </summary>
        TDestination Map(TSource source);

        /// <summary>
        /// Map a source collection to a destination collection.
        /// </summary>
        IEnumerable<TDestination> Map(IEnumerable<TSource> source);

        /// <summary>
        /// Map the source to the destination asynchronously.
        /// </summary>
        Task<TDestination> MapAsync(TSource source);

        /// <summary>
        /// Map a source collection to a destination collection asynchronously.
        /// Each item in the source collection is mapped sequentially within an asynchronous task.
        /// </summary>
        Task<IEnumerable<TDestination>> MapAsync(IEnumerable<TSource> source);

        /// <summary>
        /// Map a source collection to a destination collection asynchronously.
        /// Each item in the source collection is mapped concurrently within an asynchronous task.
        /// </summary>
         Task<IEnumerable<TDestination>> MapParallel(IEnumerable<TSource> source);
    }

}