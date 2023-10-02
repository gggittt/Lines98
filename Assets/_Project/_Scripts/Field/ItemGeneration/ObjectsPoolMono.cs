using System.Collections.Generic;
using System.Linq;
using Field.ItemGeneration.FieldItem;
using UnityEngine;

namespace Field.ItemGeneration
{
public class ObjectsPoolMono : MonoBehaviour
{
    Stack<Ball> _objects;
    Ball _prefab;

    public void Init( Ball prefab, int prewarmAmount )
    {
        _prefab = prefab;
        _objects = new Stack<Ball>();

        Prewarm( prewarmAmount );
    }

    void Prewarm( int amount )
    {
        for ( int i = 0; i < amount; i++ )
        {
            Ball obj = Create();
            obj.gameObject.SetActive( false );
        }
    }

    public Ball Get( )
    {
        Ball obj = _objects.FirstOrDefault( x => !x.isActiveAndEnabled );

        if ( obj == null )
        {
            obj = Create();
        }

        obj.gameObject.SetActive( true );
        return obj;
    }

    public void Release( Ball obj )
    {
        obj.gameObject.SetActive( false );
    }

    Ball Create( )
    {
        Ball obj = Object.Instantiate( _prefab );
        _objects.Push( obj );
        return obj;
    }
}
}