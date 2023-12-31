using System;
using System.Collections.Generic;
using Field.ItemGeneration.FieldItem;
using UnityEngine;

namespace Field
{
public class Grid<TCell>
{
    public TCell[] Cells { get; }
    public int Width { get; }
    public int Height { get; }

    public delegate bool Filter( TCell cell );

    public Grid( int width, int height )
    {
        Cells = new TCell[width * height];
        Width = width;
        Height = height;
    }

    public Grid( Vector2Int size ) : this( size.x, size.y ) { }

    public HashSet<Vector2Int> GetCoordsOfFilteredItems( Filter filterForEachItem )
    {
        var result = new HashSet<Vector2Int>();

        for ( int x = 0; x < Width; x++ )
        for ( int y = 0; y < Height; y++ )
        {
            TCell cell = Cells[ CoordsToIndex( x, y ) ];
            if ( filterForEachItem( cell ) )
            {
                result.Add( new Vector2Int( x, y ) );
            }
        }

        return result;
    }




    public int CoordsToIndex( int x, int y ) =>
        y * Width + x;

    public int CoordsToIndex( Vector2Int coords ) =>
        CoordsToIndex( coords.x, coords.y );



    public Vector2Int IndexToCoords( int index ) =>
        new Vector2Int( index % Width, index / Width );

    public void Set( int x, int y, TCell value ) =>
        Cells[ CoordsToIndex( x, y ) ] = value;

    public void Set( Vector2Int coords, TCell value ) =>
        Set( coords.x, coords.y, value );

    public void Set( int index, TCell value ) =>
        Cells[ index ] = value;


    public TCell TryGet( Vector2Int coords )
    {
        if ( IsXInBounds( coords.x ) == false )
        {
            //Debug.LogError( $"<color=cyan> x not valid: {coords.x} </color>" );
            return default;
        }

        if ( IsYInBounds( coords.y ) == false )
        {
            //Debug.LogError( $"<color=cyan> y not valid: {coords.y} </color>" );
            return default;
        }

        int index = CoordsToIndex( coords.x, coords.y );
        return Get( index );
    }

    public TCell Get( int index ) =>
        Cells[ index ];
    public TCell Get( Vector2Int coords ) =>
        TryGet( coords );

    //fixMe убрать. по вызову через "." не видно, неочевидно что этот индексатор тут есть
    public TCell this[ Vector2Int coords ] { get => TryGet( coords ); set => Set( coords.x, coords.y, value ); }


    public bool IsXInBounds( int pos ) =>
        ( pos >= 0 && pos < Width );
    public bool IsYInBounds( int pos ) =>
        ( pos >= 0 && pos < Height );

    public bool IsInBounds( Vector2Int coords ) =>
        IsXInBounds( coords.x ) && IsYInBounds( coords.y );


    public Vector2Int GetCoords( TCell value )
    {
        int i = Array.IndexOf( Cells, value );
        if ( i == - 1 )
        {
            throw new ArgumentException();
        }

        return IndexToCoords( i );
    }

}
}