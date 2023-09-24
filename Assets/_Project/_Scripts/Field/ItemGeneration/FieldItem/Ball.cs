using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Field.ItemGeneration.FieldItem
{
[ RequireComponent( typeof( BallUi ) ) ]
public class Ball : MonoBehaviour
{
    //public Vector2Int LocalCoord { get; private set; }
    //IndexToCoords в _grid = не перевод в worldPos
    [ SerializeField ] float _heightAnimation = 0.7f;


    public ShapeType Shape { get; private set; }
    public ItemRipeType RipedType { get; private set; } = ItemRipeType.Small;
    public bool Riped => RipedType == ItemRipeType.Big;

    BallUi _ballUi;
    Tween _infinitySelectLoop; //_bounceTween
    Vector3 _originPosition;

    public void Init( ShapeType shapeType, ItemRipeType itemRipeType )
    {
        Shape = shapeType;
        _ballUi.Paint( shapeType );

        RipedType = itemRipeType;

        name = ToString();
    }

    void Awake( )
    {
        _ballUi = GetComponent<BallUi>();

        _infinitySelectLoop = transform.DOLocalMoveY( .5f, .5f )
           .SetLoops( - 1, LoopType.Yoyo )
           .Pause();
    }

    public void TeleportTo( Vector2 local )
    {
        Vector3 worldPos = ToWorldPos( local ); //сейчас телепортируется. нужно передавать List<Vector2Int> по которым пройти
        transform.position = worldPos;
    }

    Vector3 ToWorldPos( Vector2 local )
    {
        Debug.Log( $"<color=cyan> {this} moved from {transform.position} to {local} </color>" );

        Debug.Log( $"<color=magenta> not implement </color>" );
        return local;
    }

    public void FollowPath( List<Vector3> path )
    {
        if ( path == null )
            return;

        for ( int i = 0; i < path.Count; i++ )
        {

        }
    }


    public void SetParentAndMoveToParent( Transform parent )
    {
        transform.SetParent( parent );
        transform.localPosition = Vector3.zero;
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

    void OnValidate( )
    {
        InitRequired();
        void InitRequired( )
        {
            if ( !_ballUi )
                _ballUi = GetComponent<BallUi>();
            // _ballUi?? = GetComponent<BallUi>(); Feature 'null-coalescing assignment' is not available. Please use language version 8.0 or greater
        }
    }

    public override string ToString( )
    {
        return $"{nameof( Ball )}, {RipedType}, {Shape}";
    }
}
}