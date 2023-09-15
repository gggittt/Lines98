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



    //[SerializeField] private BallFactory _ballFactory;
    // [ SerializeField ] BallCreator _ballCreator;
    // public Grid<Cell> Grid { get; private set; }

    void Start( )
    {
        _board.Init( _gameData.boardWidth, _gameData.boardHeight );
        _board.ItemMoved += LaunchNewTurn;

        _itemFactory.Init( _board );
        CreateDebugBalls();


        //TryCreateBalls( _gameData.startBallsAmount );

        //TestLogs( cellGrid );
    }
    void CreateDebugBalls( )
    {
        _itemFactory.CreateDebugBalls();

        //_board.ReserveCells();
    }


    void TryCreateBalls( int ballsToCreateAmount )
    {

        List<int> emptyIndexes = _board.EmptyCellsIndexes;

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