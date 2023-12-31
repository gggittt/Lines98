﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using DG.Tweening;
using Extensions;
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
    SpriteRenderer _spriteRenderer;

    void Awake( )
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _infinityBounceLoopOnSelection = transform.DOLocalMoveY( _selectionAnimationVerticalAmplitude, _selectionAnimationCycleDuration )
           .SetLoops( - 1, LoopType.Yoyo )
           .Pause();
    }

    public void FollowPath( List<Vector3> path, TweenCallback onComplete )
    {
        //.DeSelect -> StopSelectAnimation  происходит в другом месте
        Sequence sequence = DOTween.Sequence()
           .SetEase( Ease.InOutSine ); //todo cash

        Debug.Log( $"<color=cyan> {path.FormatElementsToString()} </color>" );

        foreach ( Vector3 point in path )
        {
            sequence.Append( transform
                   .DOMove( point, _moveStepDuration ) )
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
        SpritePainter.Paint( _spriteRenderer, shapeType );
    }

}
}