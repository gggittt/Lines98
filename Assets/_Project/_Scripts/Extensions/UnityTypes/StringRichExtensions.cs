using System.Collections.Generic;

namespace Extensions.UnityTypes
{
public static class StringRichExtensions
{
    const int UnityLogSize = 26;

    public static string Bold( this string str ) =>
        "<b>" + str + "</b>";

    public static string Italic( this string str ) =>
        "<i>" + str + "</i>";

    public static string Color( this string str, string color ) =>
        $"<color={color}>{str}</color>";

    public static string Size( this string str, int size = UnityLogSize ) =>
        $"<size={size}>{str}</size>";
}
}