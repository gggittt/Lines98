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