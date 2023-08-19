﻿using System;
using _Project._Scripts.Board;
using UnityEngine;

namespace _Project._Scripts
{
public class GameData : MonoBehaviour
{
    //все в одном классе - чтобы при обнове игры, или при загрузке из облака, можно было бы легко изменить данные на другие.

    //game board data

    const int MinSize = 1;
    [ SerializeField, Range( MinSize, 99 ) ]

    public int boardWidth = 9, boardHeight = 9;
    public int ballPerTurn = 3;
    public int startBallsAmount;
    //game session data
    public int score;
    public int turnIndex;
    //public ItemType[] nextTurnItemsPreview;


    void OnValidate( )
    {

    }
}
}