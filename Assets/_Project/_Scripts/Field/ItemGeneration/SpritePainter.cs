using System.Collections.Generic;
using Field.ItemGeneration.FieldItem;
using UnityEngine;

namespace Field.ItemGeneration
{
public static class SpritePainter //static чтобы не создавать экземпляр Painter для каждого шара
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

      //!! ToString {DotColor.Pink, Color.magenta.ToString()},
      //all 'rich' color list: https://docs.unity3d.com/Packages/com.unity.ugui@1.0/manual/StyledText.html

      //{DotColor.Purple, new Color(255f, 50f, 255f)},
      //{DotColor.Brown, new Color(144f, 144f, 44f)},
    };

    public static void PaintSprite( MonoBehaviour unityObject, ShapeType cellColorType )
    {
        // if ( (int) cellColorType > _dict.Count ) //ош

        if ( _dict.TryGetValue( cellColorType, out Color value ) )
        {
            unityObject.GetComponent<SpriteRenderer>().color = value;
        }
    }
}
}