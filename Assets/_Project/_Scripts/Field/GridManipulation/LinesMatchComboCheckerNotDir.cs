using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Field.GridManipulation
{
public class LinesMatchComboCheckerNotDir : ICheckAllDirections
{
    readonly PositionManager _positionManager;

    int _minLineSize;

    // readonly Board _board; //мб сюда не board а более узкофункциональный класс

    // public LinesMatchComboChecker( Func<T, List<T>> getNeighborByDirection, Direction[][] axesToCheck, Board board , int minLineSize = 5 )
    public LinesMatchComboCheckerNotDir( PositionManager positionManager, Direction[][] axesToCheck, int minLineSize )
    {
        _positionManager = positionManager;
        _minLineSize = minLineSize;
    }


    public void CheckAllDirectionsAtPoint( Vector2Int startingPosition )
    {
        Vector2Int center = startingPosition;
        HashSet<Vector2Int> allSuitableItems = new HashSet<Vector2Int>();

        //fixme ош, нужно именно попарно сравнивать! а тут 8 сразу //но для начала хотя бы проверю outOfBounds
        foreach ( Vector2Int axis in Shifts.All ) //сразу для 8
        {
            List<Vector2Int> bothLineHalves = GetItemsInLineOfSameShapeNew( axis, center );

            if ( bothLineHalves == null )
                continue;

            bothLineHalves.Add( startingPosition );
            if ( bothLineHalves.Count < _minLineSize )
                continue;

            allSuitableItems.UnionWith( bothLineHalves );
        }

        if ( allSuitableItems.Any() )
        {
            Log( "match info: ", allSuitableItems );
            return;
        }

        Debug.Log( $"<color=cyan> комбо не найдено </color>" );
    }

    List<Vector2Int> GetItemsInLineOfSameShapeNew( Vector2Int direction, Vector2Int coords )
    {
        List<Vector2Int> bothLineHalves = new List<Vector2Int>();

        Debug.Log( $"<color=green> для shift: {direction} </color>" );

        if ( _positionManager.IsInBounds( coords + direction ) == false )
        {
            Debug.Log( $"<color=orange>out of Bounds: {coords + direction} </color>" );

            return null; //если в рекурсии, то выйдет из 1 цикла ведь?
        }

        CheckNeighbour( coords, direction, bothLineHalves );


        return bothLineHalves;
    }

    void CheckNeighbour( Vector2Int origin, Vector2Int direction, List<Vector2Int> bothLineHalves )
    {
        bool isNeighborRipedAndSameShape = _positionManager.IsNeighborRipedAndSameShapeNew( origin, direction, out Vector2Int neighbour ); //ripped //1 сосед, в сторону shift
        //внутри может throw если не тот индекс

        if ( isNeighborRipedAndSameShape == false )
            return; //или break //или break для While/foreach

        bothLineHalves.Add( neighbour );
        CheckNeighbour( neighbour, direction, bothLineHalves );
    }


    void Log( string msg, IEnumerable sequence )
    {
        string result = msg;
        foreach ( object obj in sequence )
        {
            result += obj + ", ";
        }

        Debug.Log( $"<color=cyan> {result} </color>" );
    }
}
}