using System.Collections;
using System.Collections.Generic;

namespace _Project._Scripts.Field
{
class EmptyPositionFinder
{
    readonly IList _itemList;
    public EmptyPositionFinder( IList itemList ) =>
        _itemList = itemList;

    public List<int> GetFreeSpaces( ) //getNullCells
    {
        List<int> freeCellIndexes = new List<int>();

        for ( int i = 0; i < _itemList.Count; i++ )
        {
            if ( _itemList[ i ] != null )
                continue;

            freeCellIndexes.Add( i );
        }

        return freeCellIndexes;
    }
}
}