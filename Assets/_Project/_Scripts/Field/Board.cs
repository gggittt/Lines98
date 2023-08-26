using System.Collections.Generic;
using Extensions;
using Field.Cells;
using Field.ItemGeneration;
using Field.ItemGeneration.FieldItem;
using UnityEngine;

namespace Field
{
public class Board : MonoBehaviour
{
    [ SerializeField ] BallFactory _ballFactory;
    [ SerializeField ] CellCreator _cellCreator;

    Grid<Ball> _itemGrid;
    EmptyPositionFinder _emptyPositionFinder;
    //_ballFactory = new();
    Grid<Cell> _cellGrid;
    ClickManager _clickManager;

    public void Init( int width, int height )
    {
        //_cellCreator = cellCreator;
        _itemGrid = new Grid<Ball>( width, height );
        _emptyPositionFinder = new EmptyPositionFinder( _itemGrid.Cells );

        _clickManager = new ClickManager( this );

        _cellGrid = _cellCreator.CreateBoard( _clickManager, width, height );

        //_cellCreatorTransform = cellCreatorTransform; //не нужно её тут хранить. мб еще будет отступ. должен ли board Знать о нём?
    }

    public void CreateRandomBalls( int ballsAmount )
    {
        List<int> emptyIndexes = _emptyPositionFinder.GetFreeSpaces();

        if ( emptyIndexes.Count < ballsAmount )
            Debug.Log( $"<color=magenta> {emptyIndexes.Count} + {ballsAmount} > {_itemGrid.Cells.Length}  </color>" );


        for ( int i = 0; i < ballsAmount; i++ )
        {
            int positionIndex = emptyIndexes.CutRandom(); //PopRandom()?


            SetupBall( _itemGrid.IndexToCoords( positionIndex ) );
        }
    }

    void SetupBall( Vector2Int coord )
    {
        Ball ball = _ballFactory.CreateRandomBall();

        SetItemToCoord( ball, coord );

        //Debug.Log( $"<color=yellow> created {ball} </color>", ball );
        //Debug.Log( $"<color=cyan> at {parentCell} </color>", parentCell );
    }
    void SetItemToCoord( Ball ball, Vector2Int to )
    {
        _itemGrid.Set( to, ball );

        Transform parentCell = _cellGrid.Get( to ).transform;
        ball.SetParentAndMoveToParent( parentCell );
    }

    void MoveItemToNewCoord( Ball ball, Vector2Int from, Vector2Int to )
    {
        ClearOldPos();
        void ClearOldPos( ) => _itemGrid.Set( from, null );

        SetItemToCoord( ball, to );
    }

    //public Vector3 GridIndexToWorldPos( int index, Vector3 leftUpGridPos )
    //return new Vector2Int( index % _itemGrid.Width, index / _itemGrid.Width );
    //return new Vector3( coord.x + leftUpGridPos.x, - 1 * coord.y + leftUpGridPos.y, 0 );
    //хотел расписать leftUpGridPos + cellSize + ... но проще ставить родителя

    void GameOver( )
    {

    }

    public bool CanItemInCellBeSelected( Vector2Int coord )
    {
        Ball item = _itemGrid.Get( coord );

        bool heldRipeItem = item && item.ItemSizeType == ItemSizeType.Big;
        return heldRipeItem;
    }

    public void TryMoveItem( Cell itemHolder, Vector2Int to )
    {
        if ( IsPathBlocked( itemHolder.LocalCoord, to ) )
            return;
        //bool pathExist = _waveManager.TryPavePath( from: _selectedCell.LocalCoord, to, out _ );

        //bool isLineComplete = _ballMatrix.MoveBigAndSmallBalls( _selectedCell.LocalCoord, bigBallNewCoord: newCoord );

        Ball ball = _itemGrid.Get( itemHolder.LocalCoord );
        MoveItemToNewCoord( ball, from: itemHolder.LocalCoord, to );

        _clickManager.DeSelectBallInTile( itemHolder );
    }

    WaveManager _waveManager = new WaveManager();

    bool IsPathBlocked( Vector2Int from, Vector2Int to )
    {
        bool pathExist = _waveManager.TryPavePath( from, to, out _ );

        if ( pathExist )
        {
            //+ можно нанести урон?
            return false;
        }

        Debug.Log( $"<color=cyan>no path from {from} to {to}. </color>" );

        return true;
    }


}
}