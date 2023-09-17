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
            TryCreateBalls( _gameData.ballPerTurn );
        }


        var a = Direction.North + Vector2Int.zero;//сначала каст, а потом unity тупо складывает 2 вектора = итог норм
        var b = Vector2Int.zero + Direction.North; //мой operator +. выдаёт итог (0, 0), ош. //была ош * вместо +: operator +(...(pos.y * dir.Y );
        Debug.Log($"<color=cyan> {a} </color>");
        Debug.Log($"<color=cyan> {b} </color>");


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





}