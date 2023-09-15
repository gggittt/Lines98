using System;
using UnityEngine;
using UnityEditor;
using UnityEditor.ShortcutManagement;
using Object = UnityEngine.Object; //нажми по ней скм

namespace _Reusable
{
public class HotkeysShortcutsForSceneObjects : MonoBehaviour
{
    //https://docs.unity3d.com/ScriptReference/ShortcutManagement.ShortcutAttribute-ctor.html
    [ SerializeField ] UnityEngine.Object _folder_script_txt_OrCreatedSoAsset; //script, component, txt, folder, non-createdSO-script, //but cant: created SO-asset,  //try: pictures? звуки, scene, prefab, .md mind map, read.mi, ...
    [ SerializeField ] Transform _canHoldUi;

    [ SerializeField ] Object _objectToSelect;
    [ SerializeField ] MonoBehaviour _forCtrlAltE;

    static MonoBehaviour _чтобыМожноБылоВСтатичMenuItem;

    [ SerializeField ] MonoBehaviour _monoBehaviourToSelect;

    void OnValidate( )
    {
        _чтобыМожноБылоВСтатичMenuItem = _monoBehaviourToSelect;

    }



    /**
        gpt c# как поместить в статическое поле нестатический объект ? static readonly MyClass MyObject = new MyClass { MyValue = 42 }; //не вышло:    readonly static ForHotkey staticTest = new ForHotkey{  some = _monoBehaviourToSelect };

        можно еще и сразу разворачивать нужный компонент. остальные свернуть? вряд ли их будет много
        начни с тупо выбора объекта
        см код в SelectionHistory. там ебался с UnityEditor.Selection
        нужно чтобы открылся инспектор
        дожно уметь выбрать и не-MonoBehaviour
        прошерсти код всех плагинов. пг Hotkey / Shortcut / Management
     */
    void SelectObject( )
    {
        // Selection.activeGameObject = _objectToSelect;
        Selection.activeObject = _objectToSelect;
        Object[] objects = Selection.objects;
    }

    [ Shortcut( "1", KeyCode.B, (ShortcutModifiers) Mod.Ctrl ) ] //не пробовал
    void НеСтатичТест( )
    {
        Debug.Log( $"<color=cyan> {nameof( НеСтатичТест )} </color>" );

    }

    [ Shortcut( "1", KeyCode.A, (ShortcutModifiers) Mod.Ctrl ) ] //не пробовал
    static void ArgTest( ShortcutArguments args )
    {
        args.context = "Instance of the context in which the shortcut was triggered";
        var a = args.stage;
        a = ShortcutStage.Begin;

    }

    //[ MenuItem( "MENUNAME" ) ]
    // [ Shortcut("1", KeyCode.A, ShortcutModifiers.Alt) ]
    [ Shortcut( "1", KeyCode.A, (ShortcutModifiers) Mod.Shift ) ] //не пробовал
    void OnCtrlAltE( ) //будет ли работать не-static?
    {
        var ctrl = ShortcutModifiers.Action;
        var isItForSimpleKey = ShortcutModifiers.None;
        var someCode = KeyCode.Q;

        Select( _forCtrlAltE );
        Debug.Log( $"<color=cyan> {_folder_script_txt_OrCreatedSoAsset} </color>" );

    }
    void Select( MonoBehaviour toSelect )
    {
        Selection.activeObject = toSelect;
    }


    const ShortcutModifiers castTest = (ShortcutModifiers) Mod.Alt | (ShortcutModifiers) Mod.Ctrl;
    [ Shortcut( "My Shortcut", typeof( SceneView ), KeyCode.A, castTest ) ]
    [ Shortcut( "My Shortcut", typeof( SceneView ), KeyCode.A, ShortcutModifiers.Alt | ShortcutModifiers.Shift ) ]
    static void MyMethod( )
    {
        //string parameter specifies the ID of the shortcut, which is a unique string that identifies the shortcut. The System.Type parameter specifies the context of the shortcut, which is a type that defines the context in which the shortcut can be used. The context can be used to restrict the scope of the shortcut to a specific part of the Unity Editor, such as the Scene view or the Inspector window
        //shortcut is named "My Shortcut" and is bound to the "SceneView" context. When the user presses the shortcut key combination in the Scene view, the MyMethod method is called and a debug message is printed to the console. Note that the ShortcutAttribute constructor that takes a string and a System.Type parameter is only available in Unity 2020.1 and later versions
        // ShortcutArguments
        UnityEditor.ShortcutManagement.KeyCombination a = new KeyCombination();

        UnityEditor.ShortcutManagement.ShortcutArguments b = new ShortcutArguments();
        // UnityEditor.ShortcutManagement.ShortcutAttribute c = new ShortcutAttribute();
        string defaultProfileId = UnityEditor.ShortcutManagement.ShortcutManager.defaultProfileId;

        // UnityEditor.ShortcutManagement.ShortcutManager.instance. - профили
        // ShortcutAttribute.IsDefined()
        ShortcutStage shortcutStage = b.stage;
        ShortcutStage shortcutStage2 = ShortcutStage.Begin;

        Mod modificator = Mod.Alt | Mod.Ctrl; //Bitwise operation
        //пробуй что выдаст если без
        ShortcutModifiers castTest2 = (ShortcutModifiers) ( Mod.Alt | Mod.Ctrl );


        Debug.Log( "MyMethod was called!" );
    }

    /*[ Flags ] ShortcutModifiers
        None = 0,
        Alt = 1,
        Ctrl = 2,
        Shift = 4,
    */
    [ Flags ]
    enum Mod
    {
        Alt, Ctrl, Shift
      , Alone,
    }
}

}