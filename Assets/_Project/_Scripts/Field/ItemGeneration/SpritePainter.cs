using System.Collections.Generic;
using Field.ItemGeneration.FieldItem;
using UnityEngine;

namespace Field.ItemGeneration
{
public static class SpritePainter //static чтобы не создавать экземпляр Painter для каждого шара
{
    static readonly Dictionary<ItemType, Color> _dict = new Dictionary<ItemType, Color>()
    { { ItemType.Red, Color.red },
      { ItemType.Pink, Color.magenta },
      { ItemType.Blue, Color.blue },
      { ItemType.Green, Color.green },
      { ItemType.Yellow, Color.yellow },

      //!! ToString {DotColor.Pink, Color.magenta.ToString()},
      //all 'rich' color list: https://docs.unity3d.com/Packages/com.unity.ugui@1.0/manual/StyledText.html

      //{DotColor.Purple, new Color(255f, 50f, 255f)},
      //{DotColor.Brown, new Color(144f, 144f, 44f)},
    };

    public static void PaintSprite( MonoBehaviour unityObject, ItemType cellColorType )
    {
        unityObject.GetComponent<SpriteRenderer>().color = _dict[ cellColorType ];
    }
}
}