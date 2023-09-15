using System;
using System.Collections.Generic;
using System.Linq;
using Extensions;
using Field.ItemGeneration.FieldItem;
using UnityEngine;

namespace Field.ItemGeneration
{
public class ItemCreator : MonoBehaviour
{
    [ SerializeField ] Ball _prefab;
    List<ItemType> _allowedTypeToSpawnAsRandom;

    public void Init( )
    {
        CalculateAllowedTypeForRandomSpawn();
    }

    public Ball CreateBallWIthRandomColor( )
    {
        Ball newBall = GameObject.Instantiate( _prefab );

        newBall.SetColor( _allowedTypeToSpawnAsRandom.GetRandom() );
        newBall.ItemSizeType = ItemSizeType.Big;

        return newBall;
    }

    void CalculateAllowedTypeForRandomSpawn( )
    {
        _allowedTypeToSpawnAsRandom = new List<ItemType>();

        Array cellTypes = Enum.GetValues( typeof( ItemType ) );

        ItemType[] cantBeStarted =
        { ItemType.Wild, ItemType.Debug }; //forbidden

        foreach ( ItemType value in cellTypes )
        {
            if ( cantBeStarted.Contains( value ) )
                continue;

            _allowedTypeToSpawnAsRandom.Add( value );
        }
    }

}
}