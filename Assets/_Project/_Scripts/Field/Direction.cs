using UnityEngine;

public class Direction
{
    //OttoBotCode https://youtu.be/KuAsKRn9XD0?t=521

    //https://docs.unity3d.com/ScriptReference/UIElements.NavigationMoveEvent.Direction.html
    //или лучше up, down, left? стогда странно DownLeft
    public static readonly Direction North = new Direction( 0, 1 ); //у него -1, и перепутаны x-y
    public static readonly Direction East = new Direction( - 1, 0 );
    public static readonly Direction South = new Direction( 0, - 1 );
    public static readonly Direction West = new Direction( 1, 0 );

    public static readonly Direction NorthEast = North + East; //у этих длина ветора больше в 1.4 раза
    public static readonly Direction SouthEast = South + East;
    public static readonly Direction SouthWest = South + West;
    public static readonly Direction NorthWest = North + West;

    public static readonly Direction[] Sides = { North, East, South, West, };
    public static readonly Direction[] Diagonals = { NorthEast, SouthEast, SouthWest, NorthWest, };


    // tarodev in Pathfinding наклонный ромб IsoNode : NodeBase static readonly List<Vector2> Dirs = { new Vector2( 1, 0.5f ), new Vector2( - 1, 0.5f ), new Vector2( 1, - 0.5f ), new Vector2( - 1, - 0.5f ) };
    //а для Hexagonal?  ??Pos = _q * new Vector2( Sqrt3, 0 ) + _r * new Vector2( Sqrt3 / 2, 1.5f );



    public int X { get; } //ColumnDelta
    public int Y { get; } //RowDelta

    public Direction( int x, int y )
    {
        X = x;
        Y = y;
    }

    public Direction( Vector2Int dir ) : this( dir.x, dir.y )
    {
    }



    // public static Direction ForEachDirection( Func< float> heuristicDistance )
    // {
    //     foreach ( Direction direction in All )
    //     {
    //
    //     }
    // }
    // хотел:
    // for ( int i = - 1; i < 2; i++ )
    // for ( int j = - 1; j < 2; j++ )
    //     if ( i == 0 && j == 0 )
    //          continue;
    //     Logic( i, j )


    public static Direction operator +( Direction dir1, Direction dir2 ) =>
        new Direction( dir1.X + dir2.X, dir2.Y + dir1.Y );

    public static Direction operator *( int factor, Direction dir ) =>
        new Direction( factor * dir.X, factor * dir.Y );

    public static Direction operator *( Direction dir, int factor ) =>
        factor * dir;

    public static Vector2Int operator +( Vector2Int pos, Direction dir ) =>
        new Vector2Int( pos.x + dir.X, pos.y * dir.Y );
    //operator +: Vector2, Vector3, Vector3Int

    public static implicit operator Vector2Int( Direction self ) //explicit
        => new Vector2Int( self.X, self.Y );

    void CallTest( )
    {
        //Vector2Int a = Vector2Int.zero + Direction.NorthEast * 4;

    }
}