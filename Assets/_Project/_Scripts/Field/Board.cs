using System.Collections.Generic;
using System.Linq;
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
    [ SerializeField ] ClickManager _clickManager;
    public event System.Action ItemMoved;

    public List<Vector2Int> EmptyCellsIndexes => _positionsManager.GetEmptyCellsIndexes();
    //public Vector2Int GridSize => new Vector2Int( _itemGrid.Width, _itemGrid.Height );



    Grid<Ball> _itemGrid;
    Grid<Cell> _cellGrid;
    PositionManager _positionsManager;
    ICheckAllDirections _matchChecker;
    Pathfinder<Vector2Int> _pathfinder;


    public void Init( Vector2Int size )
    {
        _itemGrid = new Grid<Ball>( size );

        // _clickManager = new ClickManager( this );
        _clickManager.Init( this );

        _cellGrid = _cellCreator.CreateBoard( _clickManager, size );

        _positionsManager = new PositionManager( _itemGrid, _cellGrid );

        // _linesMatchComboChecker = new LinesMatchComboCheckerNotDir( _positionsManager, Direction.AllAxes, 3 );
        _matchChecker = new MatchChecker( _positionsManager, Direction.AllAxes, 3 );

        _pathfinder = new Pathfinder<Vector2Int>( _positionsManager.GetManhattanDistance, _positionsManager.GetConnectedFreeNodesAndStepCosts );

        //_cellCreatorTransform = cellCreatorTransform; //не нужно её тут хранить. мб еще будет отступ. должен ли board Знать о нём?
    }



    void ClearAt( Cell cell )
    {
        cell.Ball = null;
        _itemGrid.Set( cell.LocalCoord, null );
    }

    public void SetItemToCoord( Ball ball, Vector2Int to )
    {
        Ball oldBall = _itemGrid.TryGet( to );

        Cell newParentCell = _cellGrid.TryGet( to );
        newParentCell.Ball = ball;
        _itemGrid.Set( to, ball );

        if ( oldBall )
        {
            Debug.LogWarning( $"<color=cyan> in coords {to} already was {oldBall} !</color>" );
        }
    }



    public bool CanItemInCellBeSelected( Cell cell )
    {
        Ball item = cell.Ball;

        bool heldRipeItem = item && item.RipedType == ItemRipeType.Big;
        return heldRipeItem;
    }

    public void TryMoveItem( Cell from, Cell target )
    {
        Vector2Int targetCoords = target.LocalCoord;
        Path<Vector2Int> path = _pathfinder.GenerateAStarPath( from.LocalCoord, targetCoords );
        //todo точки на Cell, кнопка подтверждения
        //offer suggestPath.
        //min2win шар телепортируется, а путь показывают точки!
        //чтобы не предлагал похожие пути, во время фазы предложения игрок кликает на пустые клетки - и из них пытаться сделать путь.

        Debug.Log( $"<color=cyan> {path} </color>" );

        if ( path.IsSucceed == false )
        {
            return;
        }


        _clickManager.DeSelect( from );

        MoveItem( from, path.PathData );


        MatchInfo matched = _matchChecker.CheckAllDirectionsAtPoint( targetCoords );
        //MatchReaper.Reap( matched );

        // ItemMoved?.Invoke();
    }

    void MoveItem( Cell from, List<Vector2Int> path )
    {
        Ball ball = from.Ball;

        ClearAt( from );
        SetItemToCoord( ball, path.Last() );
        // TeleportItemTo( ball, path.Last() );

        List<Transform> pathTransforms = ( path
               .Select( coords => _cellGrid[ coords ].transform ) )
           .ToList();

        ball.Ui.FollowPath( pathTransforms, OnMove );

        void OnMove( )
        {
            SetItemTo( ball, path.Last() );
            ItemMoved?.Invoke();
        }
    }

    public void SetItemTo( Ball ball, Vector2Int to )
    {
        ball.SetParentAndMoveToParent( _cellGrid[ to ].transform );
    }



}
}