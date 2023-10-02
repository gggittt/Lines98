using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Field.ItemGeneration
{
public class ObjectsPoolGenericMono<T> : MonoBehaviour
    where T : MonoBehaviour
{
    Stack<T> _objects;
    T _prefab;

    public void Init( T prefab, int prewarmAmount )
    {
        _prefab = prefab;
        _objects = new Stack<T>();

        Prewarm( prewarmAmount );
    }

    void Prewarm( int amount )
    {
        for ( int i = 0; i < amount; i++ )
        {
            T obj = Create();
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