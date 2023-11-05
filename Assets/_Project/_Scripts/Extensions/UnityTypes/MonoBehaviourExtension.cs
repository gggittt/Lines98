using UnityEngine;

namespace Extensions.UnityTypes
{
public static class MonoBehaviourExtension
{
    public static void Enable( this MonoBehaviour self )
    {
        self.enabled = true;
    }
    public static void Disable( this MonoBehaviour self )
    {
        self.enabled = false;
    }
}
}