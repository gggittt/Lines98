using Field.ItemGeneration.FieldItem;
using UnityEngine;

namespace Field.Cells
{
public class Cell : MonoBehaviour
{
    public Vector2Int LocalCoord { get; set; }
    public bool HasItem { get; } //для анализа логики при клике. для проверки линий - см Grid<Item>
    public Ball Ball { get; set; }

    public event System.Action<Cell> Clicked;

    void OnMouseUpAsButton()
    {
        Clicked?.Invoke( this );
    }


    public override string ToString( )
    {
        var result = nameof(Cell) + LocalCoord;
        if ( HasItem )
        {
            return result + Ball;
        }

        return result + ", empty";
    }


}
}
