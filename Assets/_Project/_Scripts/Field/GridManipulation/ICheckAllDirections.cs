using System.Collections.Generic;
using UnityEngine;

namespace Field.GridManipulation
{
public interface ICheckAllDirections
{
    MatchInfo CheckAllDirectionsAtPoint( Vector2Int startingPosition );
}
}