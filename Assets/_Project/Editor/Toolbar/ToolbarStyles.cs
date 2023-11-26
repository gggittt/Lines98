using UnityEngine;

namespace UnityToolbarExtender.Examples
{
static class ToolbarStyles
{
    public static readonly GUIStyle CommandButtonStyle;
    public static readonly GUIStyle CommandButtonStyle2;
    //default fixedWidth ~= 33.
    //1 буква ~= 15

    static ToolbarStyles( )
    {
        CommandButtonStyle = new GUIStyle( "Command" ) { fontSize = 20, alignment = TextAnchor.MiddleCenter, imagePosition = ImagePosition.ImageAbove, fontStyle = FontStyle.Bold, fixedWidth = 80, };
        CommandButtonStyle2 = new GUIStyle( "Command" ) { fontSize = 22, alignment = TextAnchor.MiddleCenter, imagePosition = ImagePosition.ImageAbove, fontStyle = FontStyle.Bold, fixedWidth = 80, };
        //изучи все поля. clipping = TextClipping.Overflow -> но это только текст а не кнопка,
    }
}

}