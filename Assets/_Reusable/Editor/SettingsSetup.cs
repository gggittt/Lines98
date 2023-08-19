using UnityEditor;
using UnityEngine;

namespace _Project._Scripts.Editor
{
public static class SettingsSetup
{
    const string SetupFolderName = "Setup/";


    [ MenuItem( SetupFolderName + nameof( InitAllSettings ) ) ]
    static void InitAllSettings( )
    {
        Debug.Log( $"<color=cyan> test1 </color>" );

        EditorSettings.enterPlayModeOptionsEnabled = true;
        //device = any Android Device
    }

    static void SetNewGameObjectAtOrigin(  )
    {
        //https://youtu.be/fmbYlYU7z9Y?list=PLfmYNuLHEy-PQ6j6kki9kmM3Z5CayRSI0&t=361
        //set new GO position at (0,0,0)
        //Scene View - General - Create Objects at Origin
        //+см и др там
    }

    [ MenuItem( SetupFolderName + nameof( LogAllSettings ) ) ]
    static void LogAllSettings( )
    {
        string device = EditorSettings.unityRemoteDevice;
        Debug.Log( nameof( device ) + " = " + device );

        string applicationIdentifier = PlayerSettings.applicationIdentifier;
        Debug.Log( nameof( applicationIdentifier ) + " = " + applicationIdentifier );

        string companyName = PlayerSettings.companyName;
        Debug.Log( nameof( companyName ) + " = " + companyName );

        string productName = PlayerSettings.productName;
        Debug.Log( nameof( productName ) + " = " + productName );

        bool isFastPlayMode = EditorSettings.enterPlayModeOptionsEnabled;
        Debug.Log( nameof( isFastPlayMode ) + " = " + isFastPlayMode );



        /*
        string projectRootFolderName = "projectRootFolder invalid";
        string namespaceName = "namespace Name invalid";
        //string companyName = "company Name invalid";
        string authorName = "author Name invalid";
        */
    }



}
}