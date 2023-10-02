using System;
using Field.ItemGeneration;
using UnityEngine;

public class GameData : MonoBehaviour //SessionData? разбить с LevelData
{
    //все в одном классе - чтобы при обнове игры, или при загрузке из облака, можно было бы легко изменить данные на другие.

    //game board data

    const int MinSize = 1;
    [ SerializeField, Range( MinSize, 99 ) ]
    public int boardWidth = 9, boardHeight = 9;

    public int ballPerTurn = 3;
    public int startBallsAmount = 10;
    //game session data
    public int score;
    public int turnIndex;
    public NewBallPosType NewBallPosType;
    //public ItemType[] nextTurnItemsPreview;


    void OnValidate( )
    {

    }
}


