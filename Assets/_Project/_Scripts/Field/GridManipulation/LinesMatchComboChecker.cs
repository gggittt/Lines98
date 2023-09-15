using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Field.GridManipulation
{
public class LinesMatchComboChecker
{
    // readonly Func<T, List<T>> _getNeighborByDirection;
    readonly PositionManager _positionManager;
    Direction[][] _axesToCheck;

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

        Log( "match info: " , (IEnumerable<object>) allSuitableItems );
    }

    void Log( string msg, IEnumerable<object> sequence )
    {
        string result = msg;
        foreach ( object obj in sequence )
        {
            result += obj + ", ";
        }

        Debug.Log($"<color=cyan> {result} </color>");
    }

    List<Vector2Int> GetItemsInLineOfSameShape( Direction[] axis, Vector2Int coords )
    {
        List<Vector2Int> bothLineHalves = new List<Vector2Int>();

        foreach ( Direction direction in axis ) //2 раза
        {
            Debug.Log($"<color=green> для shift: {direction} </color>");

            if ( _positionManager.IsInBounds( coords + direction ) == false )
                break; //если в рекурсии, то выйдет из 1 цикла ведь?

            CheckNeighbour( coords );

            void CheckNeighbour( Vector2Int origin )
            {
                bool isNeighborRipedAndSameShape = _positionManager.IsNeighborRipedAndSameShape( origin, direction, out Vector2Int neighbour ); //ripped //1 сосед, в сторону shift
                //внутри может throw если не тот индекс

                if ( isNeighborRipedAndSameShape == false )
                    return; //или break //или break для While/foreach

                bothLineHalves.Add( neighbour );
                CheckNeighbour( neighbour );
            }
        }

        return bothLineHalves;
    }




}
}