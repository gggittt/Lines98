using Field.ItemGeneration;
using Field.ItemGeneration.FieldItem;
using UnityEngine;
using Zenject;

namespace TestDebug2
{
public class Some : MonoBehaviour
{

}

public class Some2 : MonoBehaviour
{
    ObjectsPoolGenericMono<Ball> _pool;

    [ Inject ] void Construct( ObjectsPoolGenericMono<Ball> pool )
    {
        _pool = pool;

    }
}
}