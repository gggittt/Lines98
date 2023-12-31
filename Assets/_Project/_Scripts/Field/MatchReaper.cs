﻿using System;
using System.Collections.Generic;
using Field.GridManipulation;
using Field.GridManipulation.MatchCheck;
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

    public MatchReaper( )
    {
        Debug.Log($"<color=cyan> Initialized {nameof(MatchReaper)} </color>");
    }

    public static void Reap( MatchInfo matchInfo )
    {
        int itemCount = matchInfo.AllSuitableItems.Count;
        //Score.Add(_scoreForMatch[itemCount]);

        //вместо Vector2Int - IPoolable/IObjectPool
        foreach ( Vector2Int item in matchInfo.AllSuitableItems )
        {
            // _pool.Send( item );

            // Ball ball = _itemGrid[ item ];
            // _itemGrid[ item ] = null;
            // _cellGrid[ item ].Ball = null;
            // ball.gameObject.Destroy();
        }
    }
}
}