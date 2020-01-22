using System;
using System.Collections.Generic;
using System.Linq;

namespace ExplicitMapper.DependencyInjection
{
    public static class Extensions
    {
        /// <summary>
        /// Given a <paramref name="type"/>, returns all <see cref="IMapper"/> closed generic interfaces it implements.
        /// If no <see cref="IMapper"/> closed generic interfaces are implemented and empty collection is returned.
        /// </summary>
        public static IEnumerable<Type> GetMappers(this Type type)
        {
            return type == null ? Array.Empty<Type>()
                : type.GetInterfaces().Where(i => i.IsGenericType
                                               && i.GetGenericTypeDefinition() == typeof(IMapper<,>));
        }
    }
}