using System.Collections.Generic;
using Field.ItemGeneration.FieldItem;
using UnityEngine;

namespace Field.ItemGeneration
{
public static class SpritePainter
{
    static readonly Dictionary<ShapeType, Color> _dict = new Dictionary<ShapeType, Color>()
    { { ShapeType.Red, Color.red },
      { ShapeType.Pink, Color.magenta }
     ,
      { ShapeType.Blue, Color.blue }
     ,
      { ShapeType.Green, Color.green }
     ,
      { ShapeType.Yellow, Color.yellow }
     ,
    };

    public static void Paint( SpriteRenderer renderer, ShapeType shapeType )
    {
        if ( _dict.TryGetValue( shapeType, out Color value ) )
        {
            renderer.color = value;
        }
    }
}
}