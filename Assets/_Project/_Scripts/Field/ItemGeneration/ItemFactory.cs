using System.Collections.Generic;
using System.Linq;
using Extensions;
using Field.ItemGeneration.FieldItem;
using UnityEngine;

namespace Field.ItemGeneration
{
public class ItemFactory : MonoBehaviour
{
    [ SerializeField ] ItemCreator _itemCreator;
    Board _board;

    public void Init( Board board )
    {
        _board = board;
        _itemCreator.Init();
    }

    public void CreateRandomBalls( int ballsAmount, List<Vector2Int> emptyIndexes )
    {
        Vector2Int[] randomIndexes = emptyIndexes.CutRandomRange( ballsAmount );

        for ( int i = 0; i < ballsAmount; i++ )
        {
            CreateBall( randomIndexes[ i ] );
        }
    }

    void CreateBall( Vector2Int indexInGrid )
    {
        Ball ball = _itemCreator.CreateSmallItemWithRandomColor();

        _board.SetItemToCoord( ball, indexInGrid );
        _board.SetItemTo( ball, to: indexInGrid );

    }

    public void CreateDebugItems( )
    {
        foreach ( Vector2Int indexInGrid in GetDebugBallsPositions() )
        {
            Ball ball = _itemCreator.CreateRipedDebugItem();

            _board.SetItemToCoord( ball, indexInGrid );
            _board.SetItemTo( ball, to: indexInGrid );
        }
    }

    List<Vector2Int> GetDebugBallsPositions( )
    {
        List<Vector2Int> blocked00And01 = new List<Vector2Int>
        { new Vector2Int( 0, 2 ), //мб как нестерук писал месяцы?: a.1, g.4, c.7
          new Vector2Int( 1, 0 )
        , new Vector2Int( 1, 1 ), };

        List<Vector2Int> cornerForPathfinding = new List<Vector2Int>
        { new Vector2Int( 0, 6 )
        , new Vector2Int( 1, 6 )
        , new Vector2Int( 1, 7 ), }; //target = 0,7. пройти: 2,8 -> 1,8 -> 0,8 -> 0,7
        List<Vector2Int> wholeRightLine = new List<Vector2Int>
        { new Vector2Int( 8, 0 )
        , new Vector2Int( 8, 1 )
        , new Vector2Int( 8, 2 )
        , new Vector2Int( 8, 7 )
        , new Vector2Int( 8, 8 ) };


        return blocked00And01
           .Concat( cornerForPathfinding )
           .Concat( wholeRightLine )
           .ToList();
    }
}
}