using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Extensions;
using UnityEngine;

namespace Field.GridManipulation
{
public class MatchChecker : ICheckAllDirections
{
    // readonly Func<T, List<T>> _getNeighborByDirection;
    readonly PositionManager _positionManager;
    readonly Direction[][] _axesToCheck;
    readonly int _minLineSize;


    // public LinesMatchComboChecker( Func<T, List<T>> getNeighborByDirection, Direction[][] axesToCheck , int minLineSize = 5 )
    public MatchChecker( PositionManager positionManager, Direction[][] axesToCheck, int minLineSize )
    {
        _positionManager = positionManager;
        _axesToCheck = axesToCheck;
        _minLineSize = minLineSize;
    }


    public MatchInfo CheckAllDirectionsAtPoint( Vector2Int startingPosition )
    {
        // HashSet<Vector2Int> allSuitableItems = new HashSet<Vector2Int>();

        MatchInfo matchInfo = new MatchInfo(startingPosition);

        //fixme ош, нужно именно попарно сравнивать! а тут 8 сразу //но для начала хотя бы проверю outOfBounds
        foreach ( Direction[] axis in _axesToCheck )
        {
            // List<Vector2Int> bothLineHalves = GetItemsInLineOfSameShapeNew( axis, center );
            HashSet<Vector2Int> firstPart = TryGetNeighboursOfConsistentlySameShape( axis[ 0 ], startingPosition );
            HashSet<Vector2Int> secondPart = TryGetNeighboursOfConsistentlySameShape( axis[ 1 ], startingPosition );
            HashSet<Vector2Int> bothLineParts = firstPart.CombineWith( secondPart );

            if ( bothLineParts == null )
                continue;

            bothLineParts.Add( startingPosition );
            if ( bothLineParts.Count < _minLineSize ) //мб проверять уже внутри match? будут лишние вызовы
                continue;

            matchInfo.Add( bothLineParts );
            // allSuitableItems.UnionWith( bothLineParts );
        }


        Debug.Log( $"{matchInfo}" );


        return matchInfo;
    }

    //sequentially consecutively serially подряд,серийно
    HashSet<Vector2Int> TryGetNeighboursOfConsistentlySameShape( Direction direction, Vector2Int coords )
    {
        HashSet<Vector2Int> suitableInHalfLine = new HashSet<Vector2Int>();

        CheckNeighbour( coords, direction, suitableInHalfLine );

        return suitableInHalfLine;
    }

    void CheckNeighbour( Vector2Int origin, Direction direction, ICollection<Vector2Int> suitableLine )
    {
        bool isNeighborRipedAndSameShape = _positionManager.IsNeighborRipedAndSameShapeNew( origin, direction, out Vector2Int neighbour ); //ripped //1 сосед, в сторону shift


        if ( isNeighborRipedAndSameShape == false )
            return; //или break для While/foreach

        Debug.Log( $"<color=gray> тот же shape в {neighbour} </color>" );


        suitableLine.Add( neighbour );
        CheckNeighbour( neighbour, direction, suitableLine );
    }




}
}