using System;
using Field.ItemGeneration.FieldItem;
using UnityEngine;

namespace Field.Cells
{
public class Cell : MonoBehaviour
{
    public Vector2Int LocalCoord { get; private set; }
    public bool HasItem => Ball != null;
    public Ball Ball { get; set; }

    public event System.Action<Cell> Clicked;

    void OnMouseUpAsButton( )
    {
        Clicked?.Invoke( this );
    }

    public void Init( Vector2Int coords )
    {
        LocalCoord = coords;
        name = $"{nameof( Cell )} {LocalCoord} ";
    }

    public override string ToString( )
    {
        if ( HasItem )
        {
            return $"{name}, hold item: {Ball}";
        }

        return name + ", empty";
    }


    public void StopSelectAnimation( )
    {
        if ( HasItem )
        {
            Ball.Ui.StopSelectAnimation();
        }
    }
    public void StartSelectAnimation( )
    {
        if ( HasItem )
        {
            Ball.Ui.StartSelectAnimation();
        }
    }

}
}