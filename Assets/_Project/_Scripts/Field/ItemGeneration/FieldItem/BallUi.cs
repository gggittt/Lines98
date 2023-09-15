using UnityEngine;

namespace Field.ItemGeneration.FieldItem
{
public class BallUi : MonoBehaviour
{
    [ SerializeField ] Vector3 _bigBallLocalScale = Vector3.one;
    [ SerializeField ] Vector3 _smallBallLocalScale = new Vector3( .4f, .4f, .4f );


    public void Paint( ShapeType shapeType )
    {
        SpritePainter.PaintSprite( this, shapeType );
    }
}
}