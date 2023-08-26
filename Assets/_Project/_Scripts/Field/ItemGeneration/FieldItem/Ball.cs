using System.Collections.Generic;
using UnityEngine;

namespace _Project._Scripts.Field.FieldItem
{
[ RequireComponent( typeof( BallUi ) ) ]
public class Ball : MonoBehaviour
{
    //public Vector2Int LocalCoord { get; private set; }
    //IndexToCoords в _grid = не перевод в worldPos

    public ItemType ItemType { get; private set; }
    public ItemSizeType BallSizeType { get; private set; } = ItemSizeType.Small;

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

    public void SetColor( ItemType itemType )
    {
        ItemType = itemType;
        _ballUi.Paint( itemType );
    }
}
}