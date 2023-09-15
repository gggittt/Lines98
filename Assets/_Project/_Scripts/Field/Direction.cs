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

    public static readonly Direction[] Sides = { North, East, South, West };
    public static readonly Direction[] Diagonals = { NorthEast, SouthEast, SouthWest, NorthWest };

    public static readonly Direction[] Vertical = { North, South };
    public static readonly Direction[] Horizontal = { West, East };
    public static readonly Direction[] DiagonalFromNorthWest = { NorthWest, SouthEast };
    public static readonly Direction[] DiagonalFromNorthEast = { NorthEast, SouthWest };

    // public List<List<Direction>> AllAxes => new List<List<Direction>>().AddRange();

    public static readonly Direction[][] AllAxes = { Vertical, Horizontal, DiagonalFromNorthWest, DiagonalFromNorthEast, };

    public static readonly Direction[,] AllAxes2 =
    { { North, South },
      { East, West, }
     ,
      { NorthWest, SouthEast }
     ,
      { NorthEast, SouthWest } };

    public static readonly Direction[,] OrthogonalAxes =
    { { North, South },
      { East, West, } }; //straight
    public static readonly Direction[,] DiagonalsAxes =
    { { NorthWest, SouthEast },
      { NorthEast, SouthWest } };


    static void ArrTest( )
    {
        foreach ( Direction direction in AllAxes2 ) //Direction[,]
        {

        }

        foreach ( Direction[] direction in AllAxes ) //Direction[][]
        {

        }
    }

    // tarodev in Pathfinding наклонный ромб IsoNode : NodeBase static readonly List<Vector2> Isometric Dirs = { new Vector2( 1, 0.5f ), new Vector2( - 1, 0.5f ), new Vector2( 1, - 0.5f ), new Vector2( - 1, - 0.5f ) };
    //отдельные class IsometricDirection
    //а для Hexagonal?  ??Pos = _q * new Vector2( Sqrt3, 0 ) + _r * new Vector2( Sqrt3 / 2, 1.5f );



    public int X { get; } //ColumnDelta
    public int Y { get; } //RowDelta

    public Direction( int x, int y )
    {
        //check if x or y > 1?  or if both 0
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

    public override string ToString( )
    {
        string result = "";
        if ( this == North )
            result = nameof( North );
        else if ( this == East )
            result = nameof( East );
        else if ( this == South )
            result = nameof( South );
        else if ( this == West )
            result = nameof( West );
        else if ( this == NorthEast )
            result = nameof( NorthEast );
        else if ( this == SouthEast )
            result = nameof( SouthEast );
        else if ( this == SouthWest )
            result = nameof( SouthWest );
        else if ( this == NorthWest )
            result = nameof( NorthWest );

        return result;
        // return $"dir: ({X}, {Y})";
    }
    // public override string ToString( ) => $"dir: ({X}, {Y})";

    void CallTest( )
    {
        //Vector2Int a = Vector2Int.zero + Direction.NorthEast * 4;

    }
}

public class Creature
{
    readonly int[] _stats = new int[3];//тогда уж Dictionary

    public ref int Strength => ref _stats[ strengthIndex ];
    const int strengthIndex = 0;

}