using System.Collections.Generic;
using Field.ItemGeneration.FieldItem;
using UnityEngine;

namespace Field.GridManipulation
{
public class PositionManager //<T>
{
    // readonly IReadOnlyList<T> _grid;
    readonly Grid<Ball> _grid;

    public PositionManager( Grid<Ball> grid )
    {
        _grid = grid;
    }


    public List<Vector2Int> GetEmptyCellsIndexes( ) //getNullCells //immatureItemPosition //GetCellsWithoutItemsIndexes
    {
        List<Vector2Int> freeCellIndexes = new List<Vector2Int>(); //мб HashSet?

        for ( int i = 0; i < _grid.Cells.Length; i++ )
        {
            if ( _grid.Cells[ i ] != null )
                continue;

            //if ( ForbiddenToSpawn.Contains( _itemList[ i ] ) )
            //    continue;

            freeCellIndexes.Add( _grid.IndexToCoords( i ) ); //изначально отсюда тащил int по всей цепочке
        }

        return freeCellIndexes;
    }


    bool GetItemOfType( )
    {
        bool result = false;
        return result;
    }

    bool GetConnected( )
    {
        bool result = false;
        return result;
    }

    bool GetManhattanDistance( )
    {
        bool result = false;
        return result;
    }

    bool CanItemInCellBeSelected( )
    {
        bool result = false;
        return result;
    }

    float GetManhattanDistance( Vector2Int t1, Vector2Int t2 ) =>
        Mathf.Abs( t1.x - t2.x ) + Mathf.Abs( t1.y - t2.y ); //только прямо, не по диагонали

    Dictionary<Vector2Int, float> GetConnectedNodesAndStepCosts( Vector2Int item )
        => GetSideNodesAndCosts( item );


    Dictionary<Vector2Int, float> GetSideNodesAndCosts( Vector2Int item )
    {
        //мб тупо в структру и в List<NodeWithCost>. и в неё GetHeuristicDistance,
        Dictionary<Vector2Int, float> adjacentNodesAndCosts = new Dictionary<Vector2Int, float>();

        foreach ( Direction side in Direction.Sides )
        {
            adjacentNodesAndCosts.Add( item + side, 1f );
        }

        return adjacentNodesAndCosts;
    }


    public bool IsNeighborRipedAndSameShape( Vector2Int origin, Direction shift, out Vector2Int neighbourCoords )
    {
        neighbourCoords = origin + shift;
        Debug.Log( $"<color=cyan> шар в: {origin}, проверка соседа: {neighbourCoords} </color>" );
        bool areCoordinatesInBounds = IsInBounds( neighbourCoords );

        Ball ball = _grid[ origin ]; //этот уж точно Riped, иначе бы не переместился
        Ball ball2 = _grid[ neighbourCoords ];
        bool areBothRiped = ball.Riped && ball2.Riped;

        return areCoordinatesInBounds && areBothRiped && AreCellsHasSameShape( ball, ball2 );
    }

    public bool IsInBounds( Vector2Int coords ) => _grid.IsInBounds( coords );

    bool AreCellsHasSameShape( Ball ball, Ball ball2 )
    {
        if ( ball.Shape == ball2.Shape )
            return true;

        return false;
    }
}
}