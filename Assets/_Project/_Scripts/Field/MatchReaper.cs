using System;
using System.Collections.Generic;
using Field.GridManipulation;
using UnityEngine;

namespace Field
{
public class MatchReaper //Collector
{
    //показать в инспекторе для ГД //или это в GameData?
    Dictionary<int, float> _scoreForMatch = new Dictionary<int, float>()
    { { 5, 5 },
      { 6, 7 },
      { 7, 9 },
      { 8, 11 },
      { 9, 14 },
    };

    public static void Reap( MatchInfo matchInfo )
    {
        int itemCount = matchInfo.AllSuitableItems.Count;
        //Score.Add(_scoreForMatch[itemCount]);

        //вместо Vector2Int - IPoolable/IObjectPool
        foreach ( Vector2Int item in matchInfo.AllSuitableItems )
        {
            // _pool.Send( item );

        }
    }
}
}