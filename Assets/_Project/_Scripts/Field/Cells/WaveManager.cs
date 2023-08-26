using System.Collections.Generic;
using UnityEngine;

namespace Field.Cells
{
class WaveManager
{
    bool _shouldItemBeTeleported = true; //for faster testing

    public bool TryPavePath( Vector2Int from, Vector2Int to, out List<Vector2Int> path )
    {
        path = new List<Vector2Int>();

        if ( _shouldItemBeTeleported )
        {
            path.Add( to );
            Debug.Log( $"<color=cyan> Teleport </color>" );
            return true;
        }

        return false;
    }

}
}