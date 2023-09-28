using Field.Cells;
using Field.GridManipulation;
using Field.ItemGeneration.FieldItem;
using UnityEngine;

namespace Field
{
public class ClickManager : MonoBehaviour
{
    Board _board; // ffixme циклическая зависимость
    //PositionManager<> positionManager

    [SerializeField] Cell _selected; //see for debug

    // public ClickManager( Board board ) => _board = board;

    public void Init( Board board )
    {
        _board = board;
    }

    public void OnCellClick( Cell cell )
    {
        Debug.Log( $"clicked item: {cell}\n selected - {_selected} ", cell );

        if ( _board.CanItemInCellBeSelected( cell ) )
        {
            ChangeSelectionTo( cell );
            return;
        }

        bool nothingSelectedForMove = _selected == null; //и прошлый и текущий раз - клик по пустому
        if ( nothingSelectedForMove )
            return;

        Debug.Log( $"<color=cyan> TryMoveItem </color>" );

        var cashed = _selected;

        _board.TryMoveItem( from: _selected, cell );
    }

    void ChangeSelectionTo( Cell cell )
    {
        DeSelect( _selected );
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


    public void DeSelect( Cell itemHolder )
    {
        if ( itemHolder )
        {
            itemHolder.StopSelectAnimation();
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