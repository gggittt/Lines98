using _Project._Scripts.Field.FieldItem;
using UnityEngine;

namespace _Project._Scripts.Field
{
public class BallFactory : MonoBehaviour
{
    [ SerializeField ] private Ball _prefab;
    public Ball CreateRandomBall( )
    {
        Ball newBall = GameObject.Instantiate( _prefab, transform );

        return newBall;

    }
}
}