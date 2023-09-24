using Field.Cells;
using Field.GridManipulation;
using Field.ItemGeneration.FieldItem;
using UnityEngine;

namespace Field
{
public class ClickManager //: MonoBehaviour //ClickHandler
{
    readonly Board _board; // ffixme циклическая зависимость
    //PositionManager<> positionManager

    Cell _selected;

    public ClickManager( Board board )
    {
        _board = board;
    }

    public void OnCellClick( Cell cell )
    {
        Debug.Log( $"clicked {cell}, selected - {_selected} ", cell );

        if ( _board.CanItemInCellBeSelected( cell ) )
        {
            SetNewBallInSelection( cell );
            return;
        }

        bool nothingSelectedForMove = _selected == null; //и прошлый и текущий раз - клик по пустому
        if ( nothingSelectedForMove )
            return;

        Debug.Log( $"<color=cyan> TryMoveItem </color>" );
        _board.TryMoveItem( itemHolder: _selected, to: cell.LocalCoord );
    }

    void SetNewBallInSelection( Cell cell )
    {
        DeSelectPrevious( );
        SelectBallInTile( cell );
    }


    // bool IsSameNodeClicked( Cell clickedTile )
    // {
    //     if ( _selected == clickedTile )
    //     {
    //         Debug.Log( $"<color=cyan> SameNodeClicked </color>" );
    //
    //         DeSelectBallInTile( _selected ); //чтобы игрока анимация не бесила
    //         return true;
    //     }
    //
    //     return false;
    // }


    public void DeSelectPrevious( )
    {
        if ( _selected )
        {
            _selected.StopSelectAnimation(); //todo вернуть в центр клетки. а то зависнет выше
        }
        _selected = null;
    }


    void SelectBallInTile( Cell clickedTile )
    {

        clickedTile.StartSelectAnimation();

        _selected = clickedTile;
    }

}
}