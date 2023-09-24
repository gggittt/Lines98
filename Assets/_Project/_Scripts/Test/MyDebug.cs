using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace Test
{
public class MyDebug : MonoBehaviour
{
    // TweenerCore<Vector3, Vector3, VectorOptions> _infinitySelectLoop;
    // Vector3 _endValue;
    // Vector3 _animationShiftOnSelection;

    Tween _infinitySelectLoop;
    bool _playing;
    Vector3 _origin;

    void Awake( )
    {
        _infinitySelectLoop = transform.DOLocalMoveY( .55f, .7f ).SetLoops( - 1, LoopType.Yoyo ).Pause();
    }

    void OnMouseUpAsButton( )
    {
        Debug.Log( _playing, this );

        if ( _playing )
        {
            StopSelectAnimation();
            _playing = false;
        }
        else
        {
            StartSelectAnimation();
            _playing = true;
        }
    }

    void StartSelectAnimation( )
    {
        // _origin = transform.position;
        _infinitySelectLoop.Play();

        // _infinitySelectLoop = transform.DOMove( _endValue, 7f ).SetLoops( - 1, LoopType.Yoyo );
        // _infinitySelectLoop.onComplete.EndInvoke( null );
    }

    void StopSelectAnimation( )
    {
        // _infinitySelectLoop.Rewind();
        // _infinitySelectLoop.Kill(); //
        _infinitySelectLoop?.Pause();
        // MoveToOriginPosition();
    }
    void MoveToOriginPosition( )
    {
        transform.position = _origin;
    }

}
}