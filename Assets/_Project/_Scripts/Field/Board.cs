using System.Collections.Generic;
using System.Linq;
using Aoiti.Pathfinding;
using Extensions;
using Field.Cells;
using Field.GridManipulation;
using Field.ItemGeneration;
using Field.ItemGeneration.FieldItem;
using UnityEngine;

namespace Field
{
public class Board : MonoBehaviour
{
    [ SerializeField ] CellCreator _cellCreator;
    public event System.Action ItemMoved;
    public List<Vector2Int> EmptyCellsIndexes => _positionsManager.GetEmptyCellsIndexes();
    public Vector2Int GridSize => new Vector2Int( _itemGrid.Width, _itemGrid.Height );

    WaveManager _waveManager = new WaveManager();

    Grid<Ball> _itemGrid;
    Grid<Cell> _cellGrid;
    ClickManager _clickManager;
    PositionManager _positionsManager;
    LinesMatchComboChecker _linesMatchComboChecker;

    // Aoiti.Pathfinding.PathfinderAoitiSpontaneous<Vector2Int> _pathfinder;

    public void Init( Vector2Int size )
    {
        _itemGrid = new Grid<Ball>(  size );

        _clickManager = new ClickManager( this );

        _cellGrid = _cellCreator.CreateBoard( _clickManager,  size );

        _positionsManager = new PositionManager( _itemGrid );
        _linesMatchComboChecker = new LinesMatchComboChecker( _positionsManager, Direction.AllAxes, 3 );

        // PathfinderAoitiSpontaneous<Vector2Int> pathfinder = new PathfinderAoitiSpontaneous<Vector2Int>( GetManhattanDistance, GetConnectedNodesAndStepCosts );

        //_cellCreatorTransform = cellCreatorTransform; //не нужно её тут хранить. мб еще будет отступ. должен ли board Знать о нём?
    }


    public void SetItemToCoord( Ball ball, int positionIndex )
    {
        SetItemToCoord( ball, _itemGrid.IndexToCoords( positionIndex ) );
    }


    public void SetItemToCoord( Ball ball, Vector2Int to )
    {
        Ball oldBall = _itemGrid.Get( to );
        if ( oldBall )
        {
            Debug.LogWarning( $"<color=cyan> in coords {to} already was {oldBall} !</color>" );
        }

        _itemGrid.Set( to, ball );

        Transform parentCell = _cellGrid.Get( to ).transform;
        ball.SetParentAndMoveToParent( parentCell );
    }

    void MoveItemToCoord( Ball ball, Vector2Int from, Vector2Int to )
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

        bool heldRipeItem = item && item.RipedType == ItemRipeType.Big;
        return heldRipeItem;
    }

    public void TryMoveItem( Cell itemHolder, Vector2Int to )
    {
        if ( IsPathBlocked( itemHolder.LocalCoord, to ) )
            return;
        //bool pathExist = _waveManager.TryPavePath( from: _selectedCell.LocalCoord, to, out _ );

        //bool isLineComplete = _ballMatrix.MoveBigAndSmallBalls( _selectedCell.LocalCoord, bigBallNewCoord: newCoord );

        Ball ball = _itemGrid.Get( itemHolder.LocalCoord );
        MoveItemToCoord( ball, from: itemHolder.LocalCoord, to );

        _clickManager.DeSelectBallInTile( itemHolder );

        //_linesMatchComboChecker.CheckAllDirectionsAtPoint( to );

        OnItemMove();

    }
    void OnItemMove( )
    {
        ItemMoved?.Invoke();
        //в LineChecker передавать не весь _itemGrid или TCell[] Cells, а только Func GetSideNodesAndCosts( Vector2Int item )
    }


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


    public List<Vector2Int> ReserveCells( )
    {
        List<Vector2Int> reservedTopCell = new List<Vector2Int>();
        List<int> reservedTopCell2 = new List<int>();

        const int topSideEndIndex = 3;
        for ( int x = 0; x < _itemGrid.Width; x++ )
        for ( int y = 0; y < topSideEndIndex; y++ )
            reservedTopCell.Add( new Vector2Int( x, y ) );

        reservedTopCell2.Add( _itemGrid.CoordsToIndex( 1.A() ) );

        List<Vector2Int> reservedBottomCell = new List<Vector2Int>();
        const int bottomSideStartIndex = 6;
        const int bottomSideEndIndex = 9;
        for ( int x = 0; x < _itemGrid.Width; x++ )
        for ( int y = bottomSideStartIndex; y < bottomSideEndIndex; y++ ) //i=6,7,8
            reservedBottomCell.Add( new Vector2Int( x, y ) );

        //_positionsFinder.ForbiddenToSpawn = reservedTopCell.Concat( reservedBottomCell );
        return null;
    }




    public bool IsInBounds( Vector2Int coords )
    {
        throw new System.NotImplementedException();
    }

    public bool IsSameType( Vector2Int origin, object neighbour )
    {
        throw new System.NotImplementedException();
    }
}
}