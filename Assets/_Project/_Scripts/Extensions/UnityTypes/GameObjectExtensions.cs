using UnityEngine;

namespace Extensions.UnityTypes
{
public static class GameObjectExtensions
{
    public static void Enable( this GameObject self )
    {
        self.SetActive( true );
    }

    public static void Disable( this GameObject self )
    {
        self.SetActive( false );
    }
}
}