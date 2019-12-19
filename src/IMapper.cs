using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExplicitMapper 
{
    public interface IMapper<TSource, TDestination>
    {
        TDestination Map(TSource source);

        IEnumerable<TDestination> Map(IEnumerable<TSource> source);

        Task<TDestination> MapAsync(TSource source);

        Task<IEnumerable<TDestination>> MapAsync(IEnumerable<TSource> source);
    }

}