using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Field.GridManipulation
{
//naming = в название отразить что он diagonalInclude
public class LinesMatchComboChecker : ICheckAllDirections
{
    // readonly Func<T, List<T>> _getNeighborByDirection;
    readonly PositionManager _positionManager;
    readonly Direction[][] _axesToCheck;

    int _minLineSize;

    // readonly Board _board; //мб сюда не board а более узкофункциональный класс

    // public LinesMatchComboChecker( Func<T, List<T>> getNeighborByDirection, Direction[][] axesToCheck, Board board , int minLineSize = 5 )
    public LinesMatchComboChecker( PositionManager positionManager, Direction[][] axesToCheck, int minLineSize )
    {
        _positionManager = positionManager;
        _axesToCheck = axesToCheck;
        _minLineSize = minLineSize;
    }


    public void CheckAllDirectionsAtPoint( Vector2Int startingPosition )
    {
        Vector2Int center = startingPosition;
        HashSet<Vector2Int> allSuitableItems = new HashSet<Vector2Int>();

        foreach ( Direction[] axis in _axesToCheck ) //для 4 направлений
        {
            List<Vector2Int> bothLineHalves = GetItemsInLineOfSameShape( axis, center ); //итого макс 4*2 штуки

            bothLineHalves.Add( startingPosition );
            if ( bothLineHalves.Count < _minLineSize )
                continue;

            allSuitableItems.UnionWith( bothLineHalves );
        }

        Log( "match info: ", allSuitableItems );
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

    List<Vector2Int> GetItemsInLineOfSameShape( Direction[] axis, Vector2Int coords )
    {
        List<Vector2Int> bothLineHalves = new List<Vector2Int>();

        foreach ( Direction direction in axis ) //в 2 противоположные стороны
        {
            Debug.Log( $"<color=green> для shift: {direction} </color>" );

            if ( _positionManager.IsInBounds( coords + direction ) == false )
            {
                Debug.Log($"<color=orange>out of Bounds: {coords + direction} </color>");

                break; //если в рекурсии, то выйдет из 1 цикла ведь?
            }

            CheckNeighbourOf( coords );

            void CheckNeighbourOf( Vector2Int origin )
            {
                bool isNeighborRipedAndSameShape = _positionManager.IsNeighborRipedAndSameShape( origin, direction, out Vector2Int neighbour ); //ripped //1 сосед, в сторону shift
                //внутри может throw если не тот индекс

                if ( isNeighborRipedAndSameShape == false )
                    return; //или break //или break для While/foreach

                bothLineHalves.Add( neighbour );
                CheckNeighbourOf( neighbour );
            }
        }

        return bothLineHalves;
    }




}
}