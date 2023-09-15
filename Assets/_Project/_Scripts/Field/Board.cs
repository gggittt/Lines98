using System.Collections.Generic;
using System.Linq;
using Aoiti.Pathfinding;
using Extensions;
using Field.Cells;
using Field.ItemGeneration;
using Field.ItemGeneration.FieldItem;
using UnityEngine;

namespace Field
{
public class Board : MonoBehaviour
{
    [ SerializeField ] CellCreator _cellCreator;
    public event System.Action ItemMoved;
    public List<int> EmptyCellsIndexes => _positionsFinder.GetCellsWithoutItemsIndexes();
    public Vector2Int GridSize => new Vector2Int( _itemGrid.Width, _itemGrid.Height );

    WaveManager _waveManager = new WaveManager();

    Grid<Ball> _itemGrid;
    Grid<Cell> _cellGrid;
    ClickManager _clickManager;
    PositionsFinder _positionsFinder;

    Aoiti.Pathfinding.PathfinderAoitiSpontaneous<Vector2Int> _pathfinder;

    public void Init( int width, int height )
    {
        _itemGrid = new Grid<Ball>( width, height );

        _clickManager = new ClickManager( this );

        _cellGrid = _cellCreator.CreateBoard( _clickManager, width, height );

        _positionsFinder = new PositionsFinder( _itemGrid.Cells );
        // _positionsFinder = new PositionsFinder( _itemGrid.Cells );

        PathfinderAoitiSpontaneous<Vector2Int> _pathfinder = new PathfinderAoitiSpontaneous<Vector2Int>(
            GetManhattanDistance, GetConnectedNodesAndStepCosts );

        //_cellCreatorTransform = cellCreatorTransform; //не нужно её тут хранить. мб еще будет отступ. должен ли board Знать о нём?
    }

    float GetManhattanDistance( Vector2Int t1, Vector2Int t2 ) =>
        Mathf.Abs( t1.x - t2.x ) + Mathf.Abs( t1.y - t2.y ); //только прямо, не по диагонали
    Dictionary<Vector2Int, float> GetConnectedNodesAndStepCosts( Vector2Int item )
        => GetSideNodesAndCosts( item );
    Dictionary<Vector2Int, float> GetSideNodesAndCosts2( Vector2Int item )
    {
        //мб тупо в структру и в List<NodeWithCost>. и в неё GetHeuristicDistance,
        Dictionary<Vector2Int, float> adjacentNodesAndCosts = new Dictionary<Vector2Int, float>
        { { new Vector2Int( item.x + 1, item.y ), 1f },
          { new Vector2Int( item.x - 1, item.y ), 1f },
          { new Vector2Int( item.x, item.y + 1 ), 1f },
          { new Vector2Int( item.x, item.y - 1 ), 1f } };

        return adjacentNodesAndCosts;
    }
    Dictionary<Vector2Int, float> GetSideNodesAndCosts( Vector2Int item )
    {
        Dictionary<Vector2Int, float> adjacentNodesAndCosts = new Dictionary<Vector2Int, float>();

        foreach ( Direction side in Direction.Sides )
        {
            adjacentNodesAndCosts.Add(side, 1f );
        }

        return adjacentNodesAndCosts;
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
        MoveItemToCoord( ball, from: itemHolder.LocalCoord, to );

        _clickManager.DeSelectBallInTile( itemHolder );

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




}
}