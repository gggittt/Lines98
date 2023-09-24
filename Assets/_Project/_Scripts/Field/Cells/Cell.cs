using Field.ItemGeneration.FieldItem;
using UnityEngine;

namespace Field.Cells
{
public class Cell : MonoBehaviour
{
    public Vector2Int LocalCoord { get; private set; }
    public bool HasItem => Ball != null; //для анализа логики при клике. для проверки линий - см Grid<Item>
    public Ball Ball { get; set; }

    public event System.Action<Cell> Clicked;

    void OnMouseUpAsButton( )
    {
        Clicked?.Invoke( this );
    }

    public void Init( Vector2Int coords )
    {
        LocalCoord = coords;
        name = LocalCoord + " " + nameof( Cell );
    }

    public override string ToString( )
    {
        var result = nameof( Cell ) + LocalCoord;
        if ( HasItem )
        {
            return result + Ball;
        }

        return result + ", empty";
    }


    public void StopSelectAnimation( )
    {
        if ( HasItem )
        {
            Ball.StopSelectAnimation();
        }
    }
    public void StartSelectAnimation( )
    {
        if ( HasItem )
        {
            Ball.StartSelectAnimation();
        }
    }

}
}