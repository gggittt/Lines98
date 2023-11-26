using UnityEditor;
using UnityEngine;

namespace UnityToolbarExtender.Examples
{
[ InitializeOnLoad ]
public class SceneSwitchLeftButton
{
    static readonly GUIContent _btn1 = new GUIContent( "Initial", "Launch from Initial scene" );
    static readonly GUIContent _btn2 = new GUIContent( "Board", "Launch Board scene" );

    static SceneSwitchLeftButton( )
    {
        ToolbarExtender.LeftToolbarGUI.Add( OnToolbarGUI );
    }

    static void OnToolbarGUI( )
    {
        GUILayout.FlexibleSpace();

        if ( GUILayout.Button( _btn1, ToolbarStyles.CommandButtonStyle ) ) //new GUIContent создаётся каждый кадр?!
        {
            SceneHelper.StartScene( "Initial" );
        }

        if ( GUILayout.Button( _btn2, ToolbarStyles.CommandButtonStyle2 ) )
        {
            SceneHelper.StartScene( "Board" );
        }
    }
}
}