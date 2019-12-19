using System;
using System.Linq.Expressions;

namespace ExplicitMapper 
{
    public static class DestinationFactory<TDestination>
        where TDestination : new()
    {
        private static readonly Func<TDestination> Factory = 
            Expression.Lambda<Func<TDestination>>(Expression.New(typeof(TDestination))).Compile();

        public static TDestination CreateInstance() => Factory();
    }

}