using Field.Cells;
using Field.GridManipulation;
using UnityEngine;

public class DebugMatchChecker : ICheckAllDirections
{
    readonly PositionManager _positionManager;

    public DebugMatchChecker( PositionManager positionManager, Direction[][] axesToCheck, int minLineSize )
    {
        _positionManager = positionManager;
    }


    public void CheckAllDirectionsAtPoint( Vector2Int startingPosition )
    {
        foreach ( Direction axis in Direction.Orthogonal )
        {
            Cell neighbourCell = _positionManager.GetNeighbourCell( startingPosition, axis );
            Debug.Log($"<color=cyan> {neighbourCell} at</color><color=red> {axis}</color>");
        }
    }
}


