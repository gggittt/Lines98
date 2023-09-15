using System;
using System.Collections.Generic;
using System.Linq;

namespace Extensions
{
public static class EnumerableExtension
{

    static readonly System.Random _random = new System.Random();

    public static T CutRandom<T>( this ICollection<T> collection ) //PopRandom()?
    {
        T element = collection.GetRandom();
        collection.Remove( element );
        return element;
    }

    public static T[] CutRandomRange<T>( this ICollection<T> collection, int amount )
    {
        T[] elements = new T [amount];

        for ( int i = 0; i < amount; i++ )
        {
            elements[i] = collection.GetRandom();
            collection.Remove( elements[i] );
        }

        return elements;
    }

    public static T GetRandom<T>( this ICollection<T> collection )
    {
        if ( collection == null )
            throw new ArgumentNullException( nameof( collection ) );

        if ( collection.Count == 0 )
            throw new InvalidOperationException( "Collection is empty" );

        int index = _random.Next( collection.Count );
        T element = collection.ElementAt( index );
        return element;
    }


}
}