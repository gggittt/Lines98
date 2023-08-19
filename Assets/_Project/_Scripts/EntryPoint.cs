using _Project._Scripts.Board;
using Unity.Mathematics;
using UnityEngine;

namespace _Project._Scripts
{

public class EntryPoint : MonoBehaviour
{
    [ SerializeField ] CellCreator _cellCreator;
    [ SerializeField ] GameData _gameData;
    //[ SerializeField ] GameBoard _board;

    //[SerializeField] private BallFactory _ballFactory;
    // [ SerializeField ] BallCreator _ballCreator;
    // public Grid<Cell> Grid { get; private set; }

    void Start( )
    {
        Grid<Cell> cellGrid = _cellCreator.CreateBoard( _gameData.boardWidth, _gameData.boardHeight ); //мб мне cellGrid не нужен? мб только itemGrid. Cell только хранит координаты, и отправляет их с событием наверх. мб не нужен даже HasItem

        //_board.CreateRandomBalls(_gameData.startBallsAmount);
        TestLogs( cellGrid );
    }

    void TestLogs( Grid<Cell> cellGrid )
    {
        Cell cell = cellGrid.Cells[ 4 ];
        Debug.Log( "cell.HasItem " + cell.HasItem, cell );
        Debug.Log( this, this );
    }

}

}