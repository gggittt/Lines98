using UnityEngine;

namespace Extensions.UnityTypes
{
public static class VectorsExtensions
{
    public static Vector3 ToVector3( this Vector2Int self )
    {
        return (Vector2) self;
    }

    public static Vector3 With( this Vector3 self, float? x = null, float? y = null, float? z = null )
    {
        return new Vector3( x ?? self.x, y ?? self.y, z ?? self.z );
    }

    public static Vector3 Add( this Vector3 self, float? x = null, float? y = null, float? z = null )
    {
        return new Vector3( self.x + ( x ?? 0 ), self.y + ( y ?? 0 ), self.z + ( z ?? 0 ) );
    }
}
}