using System;
using UnityEngine;

namespace Extensions
{
public static class IntToLetterCoordsExtension
{
    //мб переисп логику SymbolsType.ArabicNumbers из BoundCoordinatesCreator
    const string EngAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    public static Vector2Int A( this int self ) //call look like "var coords = 1.A" -> new Vector2Int(1,0)
    {
        int yIndex = EngAlphabet.IndexOf( nameof( A ) );
        return new Vector2Int( self, yIndex );
    }


}
}