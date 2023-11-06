using System.Reflection;
using Extensions.UnityTypes;
using UnityEngine;

public class Logger
{
    // static UnityEngine.Object _context;
    //todo StringBuilder, мб убрать RichColor
    // sample: Logger.Log(_debug, 1, _board, "dsf", 13242, 234234, 34, 342);


    public static void Log( params object[] args )
    {
        var result = "";

        for ( int index = 0; index < args.Length; index++ )
        {
            object arg = args[ index ];
            var color = RichColor.GetNextColor( index );
            result += arg.ToString().Color( color );
            result += ", ";
        }

        Debug.Log( result.Size() );
    }



}