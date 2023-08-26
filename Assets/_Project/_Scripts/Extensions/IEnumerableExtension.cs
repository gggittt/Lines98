using System;
using System.Collections.Generic;
using System.Linq;

namespace Extensions
{
public static class EnumerableExtension
{

    static readonly System.Random _random = new System.Random();

    public static T CutRandom<T>( this ICollection<T> collection )
    {
        if ( collection == null )
            throw new ArgumentNullException( nameof( collection ) );

        if ( collection.Count == 0 )
            throw new InvalidOperationException( "Collection is empty" );

        int index = _random.Next( collection.Count );
        T element = collection.ElementAt( index );
        collection.Remove( element );
        return element;
    }


}
}