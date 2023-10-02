using UnityEngine;

namespace Field.GridManipulation.MatchCheck
{
public interface ICheckAllDirections
{
    MatchInfo CheckAllDirectionsAtPoint( Vector2Int startingPosition );

}
}