using System;
using System.Collections.Generic;
using System.Threading;
using DG.Tweening;
using UnityEngine;

namespace Field.ItemGeneration.FieldItem
{
public class BallUi : MonoBehaviour
{
    [ SerializeField ] Vector3 _bigBallLocalScale = Vector3.one;
    [ SerializeField ] Vector3 _smallBallLocalScale = new Vector3( .4f, .4f, .4f );

    [ SerializeField ] float _selectionAnimationVerticalAmplitude = .25f;
    [ SerializeField ] float _selectionAnimationCycleDuration = .3f;
    [ SerializeField ] float _moveStepDuration = .6f;

    Tween _infinityBounceLoopOnSelection;
    Vector3 _originPosition;

    void Awake( )
    {
        _infinityBounceLoopOnSelection = transform.DOLocalMoveY( _selectionAnimationVerticalAmplitude, _selectionAnimationCycleDuration )
           .SetLoops( - 1, LoopType.Yoyo )
           .Pause();
    }

    public void FollowPath( List<Vector2Int> path, TweenCallback onComplete )
    {
        //.DeSelect -> StopSelectAnimation  происходит в другом месте
        Sequence sequence = DOTween.Sequence()
           .SetEase( Ease.InOutSine );

        foreach ( Vector2Int point in path )
        {
            sequence.Append( transform
                   .DOMove( (Vector2) point, _moveStepDuration ) )
                ; //ffixme здесь блочить инпут, на время анимации? да, т.к. игрок при долгом Move может успеть передвинуть еще 1 шар до "onComplete"
        }

        sequence.OnComplete( onComplete );
    }


    public void StartSelectAnimation( )
    {
        _originPosition = transform.position;
        _infinityBounceLoopOnSelection.Play();
    }

    public void StopSelectAnimation( )
    {
        _infinityBounceLoopOnSelection?.Pause();
        transform.position = _originPosition;
    }

    public void Paint( ShapeType shapeType )
    {
        SpritePainter.PaintSprite( this, shapeType );
    }

}
}