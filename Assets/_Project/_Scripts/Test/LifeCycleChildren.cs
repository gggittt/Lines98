using UnityEngine;

namespace Test
{
public class LifeCycleChildren : MonoBehaviour
{
    //тест если родитель выключен
    //тест если компонента если выключен GO

    [ SerializeField ] bool _disableLog;
    [ SerializeField ] string _msg = "Child";

    void LogMethodName( )
    {
        if ( _disableLog )
            return;

        var stackTrace = new System.Diagnostics.StackTrace();
        var callingMethod = stackTrace.GetFrame( 1 ).GetMethod();

        Debug.Log( _msg + $"<color=magenta> {callingMethod.Name}</color>" );
    }

    void Awake( ) => LogMethodName();
    void OnEnable( ) => LogMethodName();
    void Start( ) => LogMethodName();
    void OnDisable( ) => LogMethodName();
    void OnDestroy( ) => LogMethodName();
}
}