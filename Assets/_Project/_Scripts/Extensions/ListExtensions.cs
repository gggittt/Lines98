using JetBrains.Annotations;
using System.Collections.Generic;

namespace Extensions
{

public static class ListExtensions
{
    public static List<T> CombineWith<T>( this List<T> self, IEnumerable<T> other )
    {
        if ( self is null || other is null )
        {
            return self;
        }
        self.AddRange( other );
        return self;
    }

    public static HashSet<T> CombineWith<T>( this HashSet<T> self, IEnumerable<T> other )
    {
        if ( self is null || other is null )
        {
            return self;
        }

        self.UnionWith( other );
        return self;
    }
}
}