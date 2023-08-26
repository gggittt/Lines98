using System;
using Field.ItemGeneration.FieldItem;
using UnityEngine;

namespace Field.ItemGeneration
{
public class BallFactory : MonoBehaviour
{
    [ SerializeField ] Ball _prefab;
    public Ball CreateRandomBall( )
    {
        Ball newBall = GameObject.Instantiate( _prefab );

        newBall.SetColor( GetRandomItemType() );
        newBall.ItemSizeType = ItemSizeType.Big;

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