using System.Collections.Generic;
using UnityEngine;

namespace Extensions
{
public static class UnityObjectExtensions
{
    public static void Destroy( this Object self )
    {
        Object.Destroy( self );
    }
}
}