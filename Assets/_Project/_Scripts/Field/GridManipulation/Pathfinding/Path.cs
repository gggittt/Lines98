using System.Collections.Generic;

public struct Path<TNode>
{
    public bool IsSucceed;
    public List<TNode> PathData; //ранее List<T>. сделать тем типом, который нужен запрашивающему классу

    public override string ToString( )
    {
        string result = $"Path{nameof( IsSucceed )}: {IsSucceed}. ";

        if ( IsSucceed )
        {
            foreach ( TNode node in PathData )
                result += node + ", " ;
        }

        return result;
    }
}