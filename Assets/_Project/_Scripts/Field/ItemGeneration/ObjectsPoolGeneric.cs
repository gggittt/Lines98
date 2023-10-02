using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Field.ItemGeneration
{
public class ObjectsPoolGeneric<T>
    where T : MonoBehaviour
{
    readonly Stack<T> _objects;
    readonly T _prefab;

    public ObjectsPoolGeneric( T prefab, int prewarmObjectsAmount )
    {
        Debug.Log($"<color=cyan> Initialized {nameof( ObjectsPoolGeneric<T> )} </color>");

        _prefab = prefab;
        _objects = new Stack<T>();

        for ( int i = 0; i < prewarmObjectsAmount; i++ )
        {
            T obj = Create( );
            obj.gameObject.SetActive( false );
        }
    }

    public T Get( )
    {
        T obj = _objects.FirstOrDefault( x => !x.isActiveAndEnabled );

        if ( obj == null )
        {
            obj = Create();
        }

        obj.gameObject.SetActive( true );
        return obj;
    }

    public void Release( T obj )
    {
        obj.gameObject.SetActive( false );
    }

    T Create( )
    {
        T obj = Object.Instantiate( _prefab );
        _objects.Push( obj );
        return obj;
    }
}
}
