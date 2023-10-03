using System.Collections.Generic;
using UnityEngine;
using System;

namespace Extensions
{
public static class EnumerableExtension
{

    public static string FormatElementsToString<T>( this IEnumerable<T> self )
    {
        string result = "";

        foreach ( T element in self )
            result += element + ", ";

        return result;
    }


}
}