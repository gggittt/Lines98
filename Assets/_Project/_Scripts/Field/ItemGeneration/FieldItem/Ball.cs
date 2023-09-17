using System.Collections.Generic;
using UnityEngine;

namespace Field.ItemGeneration.FieldItem
{
[ RequireComponent( typeof( BallUi ) ) ]
public class Ball : MonoBehaviour
{
    //public Vector2Int LocalCoord { get; private set; }
    //IndexToCoords в _grid = не перевод в worldPos

    public ShapeType Shape { get; private set; } //Color // elementForm
    public ItemRipeType RipedType { get; set; } = ItemRipeType.Small; //ItemRipingType
    public bool Riped => RipedType == ItemRipeType.Big; //maturated. Ripped=разорванный

    BallUi _ballUi;

    void Awake( )
    {
        _ballUi = GetComponent<BallUi>();
    }

    public void TeleportTo( Vector2 local )
    {
        Vector3 worldPos = ToWorldPos( local ); //сейчас телепортируется. нужно передавать List<Vector2Int> по которым пройти
        transform.position = worldPos;
    }

    Vector3 ToWorldPos( Vector2 local )
    {
        Debug.Log( $"<color=cyan> {this} moved from {transform.position} to {local} </color>" );

        Debug.Log( $"<color=magenta> not implement </color>" );
        return local;
    }

    public void FollowPath( List<Vector3> path )
    {
        if ( path == null )
            return;

        for ( int i = 0; i < path.Count; i++ )
        {

        }
    }


    public void SetParentAndMoveToParent( Transform parent )
    {
        transform.SetParent( parent );
        transform.localPosition = Vector3.zero;
    }

    public void SetColor( ShapeType shapeType )
    {
        Shape = shapeType;
        _ballUi.Paint( shapeType );
    }

    public override string ToString( )
    {
        return $"{nameof( Ball )}, {RipedType}, {Shape}, ";
        //+= transform.parent + transform.position
    }

}
}