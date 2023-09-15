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
    List<ShapeType> _allowedTypeToSpawnAsRandom;
    //todo fluent builder

    public void Init( )
    {
        CalculateAllowedShapesForRandomSpawn();
    }

    public Ball CreateSmallItemWithRandomColor( )
    {
        Ball newBall = GameObject.Instantiate( _prefab );

        newBall.SetColor( _allowedTypeToSpawnAsRandom.GetRandom() );
        newBall.RipedType = ItemRipeType.Big;

        return newBall;
    }

    public Ball CreateRipedDebugItem( )
    {
        Ball newBall = GameObject.Instantiate( _prefab );

        newBall.SetColor( ShapeType.Debug );
        newBall.RipedType = ItemRipeType.Big;

        return newBall;
    }

    void CalculateAllowedShapesForRandomSpawn( )
    {
        _allowedTypeToSpawnAsRandom = new List<ShapeType>();

        Array cellTypes = Enum.GetValues( typeof( ShapeType ) );

        ShapeType[] cantBeStarted = { ShapeType.Wild, ShapeType.Debug }; //forbidden

        foreach ( ShapeType value in cellTypes )
        {
            if ( cantBeStarted.Contains( value ) )
                continue;

            _allowedTypeToSpawnAsRandom.Add( value );
        }
    }


}
}