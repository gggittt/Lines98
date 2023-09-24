using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Field.GridManipulation
{
public class MatchInfo
{
    List<HashSet<Vector2Int>> _allHalves = new List<HashSet<Vector2Int>>();
    //и здесь же высчитывать очки?
    HashSet<Vector2Int> _allSuitableItems = new HashSet<Vector2Int>();

    public MatchInfo( Vector2Int center )
    {
        Center = center;
    }

    public HashSet<Vector2Int> AllSuitableItems  => _allSuitableItems;
    public bool HasMatch => AllSuitableItems.Any();
    public Vector2Int Center { get; }


    public void Add( HashSet<Vector2Int> itemRange )
    {
        AllSuitableItems.UnionWith( itemRange );
    }

    public override string ToString( )
    {
        if ( HasMatch )
        {
            return "<color=green> match info: </color>" + Log( AllSuitableItems );
        }

        return "<color=pink> Combo not found </color>" ;
    }

    string Log( IEnumerable sequence )
    {
        string result = "";
        foreach ( object obj in sequence )
        {
            result += obj + ", ";
        }

        return result;
    }

}
}