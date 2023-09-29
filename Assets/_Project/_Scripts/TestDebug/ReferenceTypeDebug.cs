using System;
using System.Collections;
using System.Collections.Generic;
using Field.ItemGeneration.FieldItem;
using UnityEngine;

namespace Test
{
public class ReferenceTypeDebug : MonoBehaviour
{
    Vector2Int _structV2;
    ShapeType _enum = ShapeType.Blue;
    List<int> _list = new List<int>() { 1, 2, 3, 4 };
    int[] _arr = { 5, 6, 7 };

    void Awake( )
    {
        IList list2 = _list;
        int[] arr2 = _arr;

        SetNull( ref list2 );
        arr2 = null;

        LogAll();

        LogMetanit();

    }
    void LogMetanit( )
    {
        //https://metanit.com/sharp/tutorial/2.16.php
        //следует учитывать при передаче параметров по значению и по ссылке
        Person p = new Person { name = "Tom", age = 23 };
        ChangePerson( p );

        Debug.Log( $"<color=cyan> {p.name} </color>" ); // Alice
        Debug.Log( $"<color=cyan> {p.age} </color>" ); // 23
    }

    void ChangePerson( Person person )
    {
        // сработает
        person.name = "Alice";
        // сработает только в рамках данного метода
        person = new Person { name = "Bill", age = 45 };
        Debug.Log( $"<color=cyan> {person.name} </color>" ); // Bill
    }

    class Person
    {
        public string name = "";
        public int age;
    }

    void Simple( )
    {
        _list[ 0 ] = 99;
        _arr[ 0 ] = 88;
    }


    void SetNull( ref IList obj )
    {
        obj = null;
    }

    void LogAll( )
    {
        LogNullCheck( _list );

    }

    void LogNullCheck( List<int> obj )
    {
        if ( obj is null )
        {
            Debug.Log( $"<color=red> null: {nameof( obj )} </color>" );
        }
        else
        {
            Debug.Log( $"<color=lime> valid: {nameof( obj )} </color>" );
        }
    }



}
}