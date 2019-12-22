using System;
using System.Linq.Expressions;

namespace ExplicitMapper 
{
    /// <summary>
    /// Shamelessly stolen from https://github.com/Dotnet-Boxed/Framework/blob/master/Source/Boxed.Mapping/Factory.cs
    /// </summary>
    public static class DestinationFactory<TDestination>
        where TDestination : new()
    {
        private static readonly Func<TDestination> Factory = 
            Expression.Lambda<Func<TDestination>>(Expression.New(typeof(TDestination))).Compile();

        public static TDestination CreateInstance() => Factory();
    }

}