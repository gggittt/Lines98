﻿using System;
using System.Collections.Generic;
using Field.Cells;
using Field.GridManipulation.Pathfinding;
using Field.ItemGeneration.FieldItem;
using UnityEngine;

namespace Field.GridManipulation
{
public class PositionManager //<T>
{
    // readonly IReadOnlyList<T> _grid;
    readonly Grid<Ball> _itemGrid;
    readonly Grid<Cell> _cellGrid;
    Pathfinder<Vector2Int> _pathfinder;


    public PositionManager( Grid<Ball> itemGrid, Grid<Cell> cellGrid )
    {
        _itemGrid = itemGrid;
        _cellGrid = cellGrid;
    }


    public List<Vector2Int> GetEmptyCellsIndexes( ) //getNullCells //immatureItemPosition //GetCellsWithoutItemsIndexes
    {
        List<Vector2Int> freeCellIndexes = new List<Vector2Int>(); //мб HashSet?

        for ( int i = 0; i < _itemGrid.Cells.Length; i++ )
        {
            if ( _itemGrid.Cells[ i ] != null )
                continue;

            //if ( ForbiddenToSpawn.Contains( _itemList[ i ] ) )
            //    continue;

            freeCellIndexes.Add( _itemGrid.IndexToCoords( i ) ); //изначально отсюда тащил int по всей цепочке
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

    public float GetManhattanDistance( Vector2Int t1, Vector2Int t2 ) =>
        Mathf.Abs( t1.x - t2.x ) + Mathf.Abs( t1.y - t2.y ); //только прямо, не по диагонали

    public List<Vector2Int> GetConnectedFreeNodesAndStepCosts( Vector2Int item )
        => GetFreeSideNodesAndCosts( item );


    List<Vector2Int> GetFreeSideNodesAndCosts( Vector2Int item )
    {
        //мб тупо в структру и в List<NodeWithCost>. и в неё GetHeuristicDistance,
        List<Vector2Int> adjacentNodesAndCosts = new List<Vector2Int>();

        foreach ( Direction side in Direction.Orthogonal )
        {
            Vector2Int adjacent = item + side;

            bool cellExist = IsInBounds(adjacent);

            Ball ball = _itemGrid.TryGet( adjacent);
            bool walkable = ball == false || ball.Riped == false;

            if ( cellExist && walkable )
            {
                adjacentNodesAndCosts.Add( adjacent);
            }
        }
        return adjacentNodesAndCosts;
    }

    public bool IsInBounds( Vector2Int coords ) => _itemGrid.IsInBounds( coords );


    public bool IsNeighborRipedAndSameShapeNew( Vector2Int origin, Direction shift, out Vector2Int neighbourCoords )
    {
        neighbourCoords = origin + shift;
        // Debug.Log( $"<color=cyan> checking {neighbourCoords} = {shift} from {origin} </color>" );
        bool areCoordinatesInBounds = IsInBounds( neighbourCoords );
        if ( areCoordinatesInBounds == false )
        {
            return false;
        }

        Ball movedBall = _itemGrid[ origin ]; //этот уж точно Riped, иначе бы не переместился
        Ball neighbourBall = _itemGrid[ neighbourCoords ];

        if ( !movedBall )
            throw new Exception( "no moved ball" );

        if ( !neighbourBall )
        {
            return false;
        }

        bool areBothRiped = movedBall.Riped && neighbourBall.Riped;

        return areBothRiped && AreCellsHasSameShape( movedBall, neighbourBall );
    }
    public bool IsNeighborRipedAndSameShape( Vector2Int origin, Direction shift, out Vector2Int neighbourCoords )
    {
        neighbourCoords = origin + shift;
        Debug.Log( $"<color=cyan> шар в: {origin}, проверка соседа: {neighbourCoords} </color>" );
        bool areCoordinatesInBounds = IsInBounds( neighbourCoords );

        Ball movedBall = _itemGrid[ origin ]; //этот уж точно Riped, иначе бы не переместился
        Ball neighbourBall = _itemGrid[ neighbourCoords ];
        if ( !movedBall || !neighbourBall )
            return false;

        bool areBothRiped = movedBall.Riped && neighbourBall.Riped;

        return areCoordinatesInBounds && areBothRiped && AreCellsHasSameShape( movedBall, neighbourBall );
    }



    bool AreCellsHasSameShape( Ball ball, Ball ball2 )
    {
        if ( ball.Shape == ball2.Shape )
            return true;

        return false;
    }

    public Cell TryGetNeighbourCell( Vector2Int startingPosition, Vector2Int axis )
    {
        Vector2Int position = startingPosition + axis;

        if ( IsInBounds( position ) )
        {
            return _cellGrid.TryGet( position );
        }

        return null;

    }
}
}