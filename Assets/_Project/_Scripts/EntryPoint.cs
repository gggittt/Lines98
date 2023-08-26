using System.Collections.Generic;
using _Project._Scripts.Field;
using Unity.Mathematics;
using UnityEngine;

namespace _Project._Scripts
{

public class EntryPoint : MonoBehaviour
{
    [ SerializeField ] GameData _gameData;
    [ SerializeField ] Board _board;
    [ SerializeField ] UnityEngine.Object _folder_script_txt_OrCreatedSoAsset;
    [ SerializeField ] Transform _canHoldUi;

    //[SerializeField] private BallFactory _ballFactory;
    // [ SerializeField ] BallCreator _ballCreator;
    // public Grid<Cell> Grid { get; private set; }

    void Start( )
    {

        _board.Init( _gameData.boardWidth, _gameData.boardHeight  );
        _board.CreateRandomBalls( _gameData.startBallsAmount );
        //TestLogs( cellGrid );
    }

    void LaunchNewTurn( )
    {

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
        Debug.Log( "cell.HasItem " + cell.HasItem, cell );
        Debug.Log( this, this );
        Debug.Log( $"<color=cyan> {_folder_script_txt_OrCreatedSoAsset} </color>" );

    }


}

}