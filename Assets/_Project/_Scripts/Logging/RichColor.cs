using System.Drawing;

class RichColor
{
    public static string GetNextColor( int index )
    {
        return colors[ index  % colors.Length ];
    }

    public static readonly string MethodColor = "#ffff00ff";
    public static readonly string TypeColor = "#4EC9B0";

    static readonly string[] colors =
    { "#ff0000ff",
      "#ff00ffff",
      "#00ffffff",
      "#00ff00ff",
      "#ffa500ff", };
}