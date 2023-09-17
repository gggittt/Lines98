using JetBrains.Annotations;
using System.Collections.Generic;
using Field.ItemGeneration.FieldItem;
using UnityEngine;

namespace Field.ItemGeneration
{
public class ItemBuilder
{

    List<Ball> _items;

    public ItemBuilder( )
    {
        _items = new List<Ball>();
    }

    public static implicit operator List<Ball>( ItemBuilder builder )
    {
        return builder._items; //Cannot access non-static field '_items' in static context = обходить путём обращения к аргументу, а не напрямую к приватному полю = мб так можно в ExtensionMethods
    }
}
}