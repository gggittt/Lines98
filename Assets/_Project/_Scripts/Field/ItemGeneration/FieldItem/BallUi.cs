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

    [ SerializeField ] float _selectionAnimationHeight = .4f;
    [ SerializeField ] float _selectionAnimationDuration = .3f;
    [ SerializeField ] float _moveStepDuration = .6f;

    Tween _infinitySelectLoop; //_bounceTween
    Vector3 _originPosition;

    void Awake( )
    {
        _infinitySelectLoop = transform.DOLocalMoveY( _selectionAnimationHeight, _selectionAnimationDuration )
           .SetLoops( - 1, LoopType.Yoyo )
           .Pause()
           .OnComplete( ( ) =>
                Debug.Log( $"<color=grey> SelectLoop complete </color>" )
            );
    }

    // public void FollowPath( List<Vector2Int> path )
    // public void FollowPath( Transform path )
    public void FollowPath( List<Transform> path, TweenCallback onComplete )
    {
        //.DeSelect -> StopSelectAnimation  происходит в другом месте
        Sequence sequence = DOTween.Sequence()
           .SetEase( Ease.InOutSine );


        foreach ( Transform t in path )
        {
            Debug.Log( $"<color=grey> path.position was: {t.position} </color>" );
            sequence.Append( transform
                   .DOMove( t.position, _moveStepDuration ) )
                ; //здесь блочить инпут, на время анимации?
            // .OnComplete( ( ) => Debug.Log( $"<color=grey> path.position was: {path.position} </color>" ))

            // Vector3 worldPoint = FromMatrixCoordToWorldCartesian( t );
            // Thread.Sleep( _moveStepTimeout );

            // transform.localPosition += Time.deltaTime * ( transform.localPosition + worldPoint );
            // MoveTo( worldPoint );
        }

        // sequence.OnComplete( onComplete );
        sequence.onComplete += onComplete; //да, можно так, событиями
    }


    public void StartSelectAnimation( )
    {
        _originPosition = transform.position;
        _infinitySelectLoop.Play();
    }

    public void StopSelectAnimation( )
    {
        _infinitySelectLoop?.Pause();
        transform.position = _originPosition;
    }

    public void Paint( ShapeType shapeType )
    {
        SpritePainter.PaintSprite( this, shapeType );
    }

}
}