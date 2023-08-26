using System.Collections.Generic;
using _Project._Scripts.Extensions;
using _Project._Scripts.Field.FieldItem;
using UnityEngine;

namespace _Project._Scripts.Field
{
public class Board : MonoBehaviour
{
    [ SerializeField ] BallFactory _ballFactory;
    [ SerializeField ] CellCreator _cellCreator;

    Grid<Ball> _itemGrid;
    EmptyPositionFinder _emptyPositionFinder;
    //_ballFactory = new();
    Grid<Cell> _cellGrid;

    public void Init( int width, int height )
    {
        //_cellCreator = cellCreator;
        _itemGrid = new Grid<Ball>( width, height );
        _emptyPositionFinder = new EmptyPositionFinder( _itemGrid.Cells );


        _cellGrid = _cellCreator.CreateBoard( width, height );

        //_cellCreatorTransform = cellCreatorTransform; //не нужно её тут хранить. мб еще будет отступ. должен ли board Знать о нём?
    }

    public void CreateRandomBalls( int ballsAmount )
    {
        List<int> emptyIndexes = _emptyPositionFinder.GetFreeSpaces();

        if ( emptyIndexes.Count < ballsAmount )
            GameOver();


        for ( int i = 0; i < ballsAmount; i++ )
        {
            int positionIndex = emptyIndexes.CutRandom(); //PopRandom()?

            SetupBall( positionIndex );
        }
        //todo indexator itemGrid[ 1, 2 ];
    }
    void SetupBall( int positionIndex )
    {

        Ball ball = _ballFactory.CreateRandomBall();


        _itemGrid.Set( positionIndex, ball );

        //Vector2Int coords = _itemGrid.IndexToCoords( positionIndex );

        Transform parentCell = _cellGrid.Get( positionIndex ).transform;

        ball.SetParentAndMoveToParent( parentCell );

        Debug.Log( $"<color=yellow> created {ball} </color>", ball );
        Debug.Log( $"<color=cyan> at {parentCell} </color>", parentCell );

    }

    //public Vector3 GridIndexToWorldPos( int index, Vector3 leftUpGridPos )
    //return new Vector2Int( index % _itemGrid.Width, index / _itemGrid.Width );
    //return new Vector3( coord.x + leftUpGridPos.x, - 1 * coord.y + leftUpGridPos.y, 0 );
    //хотел расписать leftUpGridPos + cellSize + ... но проще ставить родителя

    void GameOver( )
    {
        throw new System.NotImplementedException();
    }

}
}