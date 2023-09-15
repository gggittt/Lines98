using System.Collections.Generic;
using System.Linq;
using Extensions;
using Field.ItemGeneration;
using Field.ItemGeneration.FieldItem;
using UnityEngine;

namespace Field
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

    public void CreateRandomBalls( int ballsAmount, List<int> emptyIndexes )
    {
        int[] randomIndexes = emptyIndexes.CutRandomRange( ballsAmount );

        for ( int i = 0; i < ballsAmount; i++ )
        {
            CreateBall( randomIndexes[ i ] );
        }
    }

    void CreateBall( int indexInGrid )
    {
        Ball ball = _itemCreator.CreateBallWIthRandomColor();

        _board.SetItemToCoord( ball, indexInGrid );
    }

    public void CreateDebugBalls( )
    {
        foreach ( Vector2Int vector2Int in GetDebugBallsPositions() )
        {
            Ball ball = _itemCreator.CreateBallWIthRandomColor();

            _board.SetItemToCoord( ball, vector2Int );
        }
    }



    List<Vector2Int> GetDebugBallsPositions( )
    {
        List<Vector2Int> blocked00And01 = new List<Vector2Int>
        { new Vector2Int( 1, 0 ),
          new Vector2Int( 1, 1 ),
          new Vector2Int( 2, 1 ),
          new Vector2Int( 0, 2 ), //мб как нестерук писал месяцы?: a.1, g.4, c.7
        };
        List<Vector2Int> wallForPathfinding = new List<Vector2Int>
        { new Vector2Int( 0, 6 ),
          new Vector2Int( 1, 6 ),
          new Vector2Int( 1, 7 ),
          //target = 0,7. пройти: 2,8 -> 1,8 -> 0,8 -> 0,7
        };

        return blocked00And01.Concat( wallForPathfinding ).ToList();
    }
}
}