using UnityEngine;

namespace _Project._Scripts.Field.FieldItem
{
public class BallUi : MonoBehaviour
{
    [ SerializeField ] Vector3 _bigBallLocalScale = Vector3.one;
    [ SerializeField ] Vector3 _smallBallLocalScale = new Vector3( .4f, .4f, .4f );


    public void Paint( ItemType itemType )
    {
        SpritePainter.PaintSprite( this, itemType );
    }
}
}