using System;
using System.Collections.Generic;
using System.Threading;
using DG.Tweening;
using DG.Tweening.Core;
using UnityEngine;

namespace Field.ItemGeneration.FieldItem
{
[ RequireComponent( typeof( BallUi ) ) ]
public class Ball : MonoBehaviour
{
    //public Vector2Int LocalCoord { get; private set; }
    //IndexToCoords в _grid = не перевод в worldPos


    public ShapeType Shape { get; private set; }
    public ItemRipeType RipedType { get; private set; } = ItemRipeType.Small;
    public bool Riped => RipedType == ItemRipeType.Big;
    public BallUi Ui { get; private set; }

    public void Init( ShapeType shapeType, ItemRipeType itemRipeType )
    {
        Shape = shapeType;

        Ui = GetComponent<BallUi>();
        Ui.Paint( shapeType );

        RipedType = itemRipeType;

        name = ToString();
    }

    /*
     * передавать List<Transform> pathParents
     *
     * 1. setParent
     * 1. setParent

     */
    [ SerializeField ] int _moveStepTimeout = 40;



    Vector3 FromMatrixCoordToWorldCartesian( Vector2Int matrix ) => new Vector3( matrix.x, - matrix.y );


    public void SetParentAndMoveToParent( Transform parent )
    {
        transform.SetParent( parent );
        transform.localPosition = Vector3.zero;
    }


    void OnValidate( )
    {
        InitRequired();

        void InitRequired( )
        {
            if ( !Ui )
                Ui = GetComponent<BallUi>();
        }
    }

    public override string ToString( )
    {
        return $"{nameof( Ball )}, {RipedType}, {Shape}";
    }
}
}