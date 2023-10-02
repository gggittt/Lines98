
using UnityEditor;
using System;
using System.Reflection;

public enum WindowType
{
    Game, Scene, Hierarchy, Console, Inspector,
}

//https://gist.github.com/Syy9/c0df54799395f0437ef5933ac61ee374
public static class EditorWindowHelper
{

    // EditorWindow GetWindow<T>(  ) where T : EditorWindow === нельзя т.к. Cannot access internal class 'SceneHierarchyWindow' here

    // ProjectWindow тут нету, т.к. оно встроено: EditorUtility.FocusProjectWindow();

    public static EditorWindow GetWindow( EditorWindow window )
    {
        string windowName = window.ToString()
           .Replace( "(", "" )
           .Replace( ")", "" )
            ;

        Assembly assembly = typeof( EditorWindow ).Assembly;
        Type type = assembly.GetType( windowName );
        return EditorWindow.GetWindow( type );
    }

    public static EditorWindow GetWindow( WindowType windowType )
    {
        string windowName = Convert( windowType );

        Assembly assembly = typeof( EditorWindow ).Assembly;
        Type type = assembly.GetType( windowName );
        return EditorWindow.GetWindow( type );
    }


    /** также есть:
     *     https://github.com/Unity-Technologies/UnityCsReference/tree/master/Editor/Mono
     * PreferencesWindow
     * ProjectWindow
     * SceneModeWindows
     * SettingsWindow
     * SceneHierarchySortingWindow.cs
     * ProjectTemplateWindow.cs
     * EditorWindow.cs
     * ContainerWindow.cs
     * BuildPlayerWindow.cs
    */
    static string Convert( WindowType windowType )
    {
        string name;
        switch ( windowType )
        {
            case WindowType.Game:
                name = "UnityEditor.GameView";
                break;
            case WindowType.Scene:
                name = "UnityEditor.SceneView";
                break;
            case WindowType.Hierarchy:
                name = "UnityEditor.SceneHierarchyWindow";
                break;
            case WindowType.Console:
                name = "UnityEditor.ConsoleWindow";
                break;
            case WindowType.Inspector:
                name = "UnityEditor.InspectorWindow";
                break;
            default:
                throw new NotImplementedException();
        }

        return name;
    }

}