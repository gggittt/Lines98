using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Field.ItemGeneration.FieldItem;
using UnityEngine;

namespace Field.GridManipulation
{
public class CellsOfTypeFinder
{
    public HashSet<Vector2Int> Reserved { get; set; }

    Grid<Ball> _grid;
    readonly IList _itemList; //=TCell[] Cells
    public CellsOfTypeFinder( IList itemList ) =>
        _itemList = itemList;

    public void Init( ) { }


    public HashSet<int> GetCellsWithoutItemsIndexes( ) //getNullCells //immatureItemPosition
    {
        HashSet<int> freeCellIndexes = new HashSet<int>();

        for ( int i = 0; i < _itemList.Count; i++ )
        {
            if ( _itemList[ i ] != null )
                continue;

            // if ( ForbiddenToSpawn.Contains( _itemList[ i ] ) )
            //     continue;

            freeCellIndexes.Add( i );
        }

        Console.WriteLine( "Console.WriteLine" );

        return freeCellIndexes;
    }


    HashSet<Vector2Int> GetIndexesWithItems( ItemRipeType requestedType ) =>
        GetIndexesWithItems( new[] { requestedType } );

    HashSet<Vector2Int> GetIndexesWithItems( ItemRipeType[] requestedTypes )
    {
        Grid<Ball>.Filter filter = IsItemOfType( requestedTypes );
        return _grid.GetCoordsOfFilteredItems( filter );
    }

    HashSet<Vector2Int> GetIndexesWithItems2( ItemRipeType[] requestedTypes )
    {
        HashSet<Vector2Int> result = new HashSet<Vector2Int>();
        foreach ( ItemRipeType type in requestedTypes )
        {
            Grid<Ball>.Filter filter = IsItemOfType( type );

            result.UnionWith(_grid.GetCoordsOfFilteredItems( filter ));
        }

        return result;
    }


    HashSet<Vector2Int> GetIndexesWithoutItems( ItemRipeType[] forbiddenTypes )
    {
        Grid<Ball>.Filter filter = IsItemNotType( forbiddenTypes );
        return _grid.GetCoordsOfFilteredItems( filter );
    }


    Grid<Ball>.Filter IsItemOfType( ItemRipeType value ) => ball => ball.RipedType == value;
    Grid<Ball>.Filter IsItemOfType( ItemRipeType[] value ) => ball =>
        value.Any( itemSizeType => ball.RipedType == itemSizeType );

    Grid<Ball>.Filter IsItemNotType( ItemRipeType value ) => ball => ball.RipedType != value;
    Grid<Ball>.Filter IsItemNotType( ItemRipeType[] value ) => ball =>
        value.Any( itemSizeType => ball.RipedType != itemSizeType );

}
}