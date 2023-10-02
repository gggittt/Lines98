using UnityEngine;

namespace Field
{
public class Direction
{
    //OttoBotCode https://youtu.be/KuAsKRn9XD0?t=521

    //https://docs.unity3d.com/ScriptReference/UIElements.NavigationMoveEvent.Direction.html мб назвать Shift

    const int fromCartesianYCoeficient = - 1; //Descartes
    public static readonly Direction Up = new Direction( 0, 1 ) * fromCartesianYCoeficient;
    public static readonly Direction Right = new Direction( 1, 0 );
    public static readonly Direction Down = new Direction( 0, - 1 ) * fromCartesianYCoeficient;
    public static readonly Direction Left = new Direction( - 1, 0 );

    public static readonly Direction UpRight = Up + Right; //у этих длина ветора больше в 1.4 раза
    public static readonly Direction DownRight = Down + Right;
    public static readonly Direction DownLeft = Down + Left;
    public static readonly Direction UpLeft = Up + Left;

    public static readonly Direction[] Orthogonal =
    { Up, Right, Down, Left };
    public static readonly Direction[] Diagonals =
    { UpRight, DownRight, DownLeft, UpLeft };

    public static readonly Direction[] Vertical =
    { Up, Down };
    public static readonly Direction[] Horizontal =
    { Left, Right };
    public static readonly Direction[] DiagonalFromNorthWest =
    { UpLeft, DownRight };
    public static readonly Direction[] DiagonalFromNorthEast =
    { UpRight, DownLeft };

    // public List<List<Direction>> AllAxes => new List<List<Direction>>().AddRange();

    public static readonly Direction[][] AllAxes =
    { Vertical, Horizontal, DiagonalFromNorthWest, DiagonalFromNorthEast, };

    public static readonly Direction[,] AllAxes2 =
    { { Up, Down },
      { Right, Left, }
     ,
      { UpLeft, DownRight }
     ,
      { UpRight, DownLeft } };

    public static readonly Direction[,] OrthogonalAxes =
    { { Up, Down },
      { Right, Left, } }; //straight
    public static readonly Direction[,] DiagonalsAxes =
    { { UpLeft, DownRight },
      { UpRight, DownLeft } };


    static void ArrTest( )
    {
        // foreach ( Direction direction in AllAxes2 ) //Direction[,]
        // foreach ( Direction[] direction in AllAxes ) //Direction[][]
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
        new Vector2Int( pos.x + dir.X, pos.y + dir.Y );
    //operator +: Vector2, Vector3, Vector3Int

    public static implicit operator Vector2Int( Direction self ) //explicit
        => new Vector2Int( self.X, self.Y );

    public override string ToString( )
    {
        string result = "";
        if ( this == Up )
            result = nameof( Up );
        else if ( this == Right )
            result = nameof( Right );
        else if ( this == Down )
            result = nameof( Down );
        else if ( this == Left )
            result = nameof( Left );
        else if ( this == UpRight )
            result = nameof( UpRight );
        else if ( this == DownRight )
            result = nameof( DownRight );
        else if ( this == DownLeft )
            result = nameof( DownLeft );
        else if ( this == UpLeft )
            result = nameof( UpLeft );

        return result;
        // return $"dir: ({X}, {Y})";
    }
    // public override string ToString( ) => $"dir: ({X}, {Y})";

    void CallTest( )
    {
        //Vector2Int a = Vector2Int.zero + Direction.NorthEast * 4;

    }
}
}