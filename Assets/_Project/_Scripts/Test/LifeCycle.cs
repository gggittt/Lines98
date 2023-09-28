using UnityEngine;

namespace Test
{
public class LifeCycle : MonoBehaviour
{
    [ SerializeField ] bool _disableLog;
    [ SerializeField ] string _msg = "Debug parent";

    void LogMethodName( )
    {
        if ( _disableLog )
            return;

        var stackTrace = new System.Diagnostics.StackTrace();
        var callingMethod = stackTrace.GetFrame( 1 ).GetMethod();

        Debug.Log( _msg + $"<color=red> {callingMethod.Name}</color>" );
        //Debug.Log( $"<color=cyan> {System.Reflection.MethodBase.GetCurrentMethod()?.Name} </color>" );
    }

    void Awake( ) => LogMethodName();
    void OnEnable( ) => LogMethodName();
    void Start( ) => LogMethodName();
    void OnDisable( ) => LogMethodName();
    void OnDestroy( ) => LogMethodName();
    // void FixedUpdate( ) => LogMethodName();
    // void Update( ) => LogMethodName();
    // void LateUpdate( ) => LogMethodName();
}
}