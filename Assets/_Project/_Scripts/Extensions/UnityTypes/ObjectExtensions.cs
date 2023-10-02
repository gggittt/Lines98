using UnityEngine;

namespace Extensions.UnityTypes
{
public static class ObjectExtensions
{
    public static void Destroy( this Object self )
    {
        Object.Destroy( self );
    }

}
}