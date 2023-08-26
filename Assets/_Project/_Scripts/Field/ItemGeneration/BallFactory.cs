using System;
using _Project._Scripts.Field.FieldItem;
using UnityEngine;

namespace _Project._Scripts.Field
{
public class BallFactory : MonoBehaviour
{
    [ SerializeField ] Ball _prefab;
    public Ball CreateRandomBall( )
    {
        Ball newBall = GameObject.Instantiate( _prefab );

        newBall.SetColor( GetRandomItemType() );

        return newBall;
    }

    ItemType GetRandomItemType( )
    {
        Array cellTypes = Enum.GetValues( typeof( ItemType ) );

        int result = UnityEngine.Random.Range( 0, cellTypes.Length );

        return (ItemType) result;
    }
}
}