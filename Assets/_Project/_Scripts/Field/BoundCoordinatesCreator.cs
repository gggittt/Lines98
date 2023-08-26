using System;
using UnityEngine;

namespace _Project._Scripts.Field
{

public enum SymbolsType
{
    ArabicNumbers,
    //RomanNumbers,
    Latin,
    Cyrillic,
}

public class BoundCoordinatesCreator : MonoBehaviour
{
    [ SerializeField ] CoordinatesNumberUi _numberPrefab;

    [ SerializeField ] SymbolsType _boundSymbolsType = SymbolsType.Latin;

    public void CreateBounds( int xCount, int yCount, Vector3 nodeCellSize )
    {
        Vector3 startPos = transform.position; //кто-то может сдвинуть коорд или в иерархии

        CreateTopBounds();
        CreateLeftBounds();

        void CreateTopBounds( )
        {
            Vector3 topBoundFirstPos = new Vector3( startPos.x, startPos.y + nodeCellSize.y );
            Vector3 horizontalUnitShift = new Vector3( nodeCellSize.x, 0 );
            CreateBounds( topBoundFirstPos, xCount, horizontalUnitShift, nodeCellSize );
        }

        void CreateLeftBounds( )
        {
            Vector3 leftBoundFirstPos = new Vector3( startPos.x - nodeCellSize.x, startPos.y );
            Vector3 verticalUnitShift = new Vector3( 0, - nodeCellSize.y );

            char[] symbols = CalculateLeftSideBoundsSymbols( yCount ).ToCharArray();
            CreateBounds( leftBoundFirstPos, yCount, verticalUnitShift, nodeCellSize, symbols );
        }
    }

    string CalculateLeftSideBoundsSymbols( int count )
    {
        string boundsSymbols = "";

        switch ( _boundSymbolsType )
        {
            case SymbolsType.ArabicNumbers:
                for ( int i = 0; i < count; i++ )
                {
                    boundsSymbols += i.ToString();
                }

                break;
            case SymbolsType.Latin:
                const string engAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                boundsSymbols = engAlphabet.Substring( 0, count );
                break;

            case SymbolsType.Cyrillic:
                const string rusAlphabet = "АБВГДЕЗЖИКЛМНОПРСТУФХЦЧШЩЭЮЯ"; //без ЁЙ ЬЪЫ
                boundsSymbols = rusAlphabet.Substring( 0, count );
                break;

            default:
                throw new NotImplementedException();
        }

        return boundsSymbols;
    }

    void CreateBounds( Vector3 startPos
      , int count
      , Vector3 unitShift
      , Vector3 nodeCellSize
      , char[] uiBoundsSymbols = null )
    {
        for ( int i = 0; i < count; i++ )
        {
            CoordinatesNumberUi numberGo = Instantiate( _numberPrefab, transform );

            Vector3 shift = unitShift * i;
            Vector3 position = startPos + shift;
            Transform cashTransform = numberGo.transform;

            cashTransform.position = position;
            cashTransform.localScale = nodeCellSize;

            if ( uiBoundsSymbols == null )
            {
                numberGo.SetUiSymbol( i );
            }
            else
            {
                numberGo.SetUiSymbol( uiBoundsSymbols[ i ].ToString() );
            }
        }
    }
}
}