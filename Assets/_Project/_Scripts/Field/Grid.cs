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

    //public Grid( int width, int height, InitFunction init )
    public delegate TCell InitFunction( int x, int y ); // замена Func < int, int, TCell >
    public delegate bool Filter( TCell cell );
    public delegate bool FilterAtCoords( Vector2Int coords );

    public Grid( int width, int height )
    {
        Cells = new TCell[width * height];
        Width = width;
        Height = height;
    }


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

    public Grid( int width, int height, InitFunction init )
    {
        Cells = new TCell[width * height];
        Width = width;
        Height = height;
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


    public TCell Get( Vector2Int coords ) =>
        Cells[ CoordsToIndex( coords.x, coords.y ) ];

    public TCell Get( int index ) =>
        Cells[ index ];


    public bool AreCoordsValid( Vector2Int coords ) =>
        ( coords.x > 0 && coords.x < Width - 1 && coords.y > 0 && coords.y < Height - 1 );


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