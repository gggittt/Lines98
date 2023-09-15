using System;
using System.Collections.Generic;
using System.Linq;
using Field;
using Field.Cells;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [ SerializeField ] GameData _gameData;
    [ SerializeField ] Board _board;
    [ SerializeField ] ItemFactory _itemFactory;
    [ SerializeField ] bool _debug;

    void Start( )
    {
        Vector2Int size = new Vector2Int( _gameData.boardWidth, _gameData.boardHeight );
        _board.Init( size );
        _board.ItemMoved += LaunchNewTurn;


        _itemFactory.Init( _board );
        if ( _debug )
        {
            _itemFactory.CreateDebugItems();
        }
        else
        {
            TryCreateBalls( 12 );
        }


        var a = Direction.North + Vector2Int.one;
        Debug.Log($"<color=cyan> {a} </color>");


        var arr = new[] { 1, 2, 3, 4 };
        for ( int i = 0; i < 4; i++ )
        {
            var variable = i;
            // arr[ i ].ToString() //1, 2, 3, 4
            //{nameof( variable ) //"variable"

            // Debug.Log( $"<color=cyan> {nameof( arr[ i ] )} </color>" );
        }

        //TryCreateBalls( _gameData.startBallsAmount );

        //TestLogs( cellGrid );
    }

    void TryCreateBalls( int ballsToCreateAmount )
    {

        List<Vector2Int> emptyIndexes = _board.EmptyCellsIndexes;

        if ( IsGameOver( ballsToCreateAmount, emptyIndexes.Count ) )
        {
            Debug.Log( $"<color=magenta> GameOver: {emptyIndexes.Count} > {ballsToCreateAmount}  </color>" );
        }

        _itemFactory.CreateRandomBalls( ballsToCreateAmount, emptyIndexes );
    }

    void LaunchNewTurn( )
    {
        TryCreateBalls( _gameData.ballPerTurn );


        //turnIndex++;
    }

    bool IsGameOver( int ballsToCreate, int emptySpacesAmount )
    {
        return ballsToCreate > emptySpacesAmount;
    }


    void TestLogs( Grid<Cell> cellGrid )
    {
        int[] arr = { 1, 2, 3 };
        IEnumerable<int> ie = arr;
        ICollection<int> coll = arr;
        IList<int> list = arr;

        Cell cell = cellGrid.Cells[ 4 ];
        Debug.Log( ie );
        Debug.Log( list.Count );
        //Debug.Log( "cell.HasItem " + cell.HasItem, cell );
        Debug.Log( this, this );

    }


}