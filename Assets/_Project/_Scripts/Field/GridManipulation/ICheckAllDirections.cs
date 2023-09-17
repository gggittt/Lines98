using UnityEngine;

namespace Field.GridManipulation
{
public interface ICheckAllDirections
{
    void CheckAllDirectionsAtPoint( Vector2Int startingPosition );
}
}