// Decompiled with JetBrains decompiler
// Type: Aoiti.Pathfinding.Pathfinder`1
// Assembly: Pathfinding, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ACA93B6B-4A4F-4F5F-A994-F5907189AFD5
// Assembly location: D:\5 Unity proj\1 Unity_Genres\2d = Sandbox test\Assets\Aoiti\Plugins\Pathfinding.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Aoiti.Pathfinding
{
struct NodeValues
{
    public float G; //true distance from origin
    public float HeuristicToEnd; //heuristic to end. "optimistic", прямой без препятствий //priorityToEnd
    public float FinalCost; //f = g+h //FinalPriority-значило бы что чем выше тем лучше
    //сделать бы метод, где
    //чем меньше - тем приоритетнее
}
public struct Path
{
    public bool IsSucceed;
    public IList Value; //ранее List<T>. сделать тем типом, который нужен запрашивающему классу
}

public class PathfinderAoitiSpontaneous<T>
{

    //https://youtu.be/P7sFfFLH4iM?t=120
    readonly int _calculatorPatience = 9_999;
    readonly Func<T, T, float> _heuristicDistance;
    readonly Func<T, Dictionary<T, float>> _connectedNodesAndStepCosts; //именно connected, т.к. связи мб не только с neighbours
    //пока нужна только: Func<T, T> _walkableNeighbours; //пока stepCost не нужен?

    public PathfinderAoitiSpontaneous( Func<T, T, float> heuristicDistance,
        Func<T, Dictionary<T, float>> connectedNodesAndStepCosts )
    {
        _heuristicDistance = heuristicDistance;
        _connectedNodesAndStepCosts = connectedNodesAndStepCosts;
    }

    public Path GenerateAStarPath( T startNode, T endNode )
    {

        float startToEndDistance = _heuristicDistance( startNode, endNode );
        int calculatorPatience = _calculatorPatience;

        HashSet<T> closedList = new HashSet<T>();

        Dictionary<T, NodeValues> openList = new Dictionary<T, NodeValues>();
        openList.Add( startNode, new NodeValues { G = 0.0f, HeuristicToEnd = startToEndDistance, FinalCost = startToEndDistance } );

        Dictionary<T, T> directions = new Dictionary<T, T>(); //вместо этого - хранить currentNode.ParentNode. но тут node просто T. мб обязать where T : INode -> Parent { get; }   float GetDistance( otherCell );

        while ( calculatorPatience > 0 ) //todo удалить? нет, можно только заменить на openList.Any()
        {
            --calculatorPatience;
            if ( openList.Count == 0 )
                break;

            //T currentNode = openList.Aggregate( ( l, r )=> l.Value.F >= (double) r.Value.F ? r : l ).Key; //нечитаемо

            KeyValuePair<T, NodeValues> valuePair = openList.Aggregate( FindBestPriority );
            T currentNode = valuePair.Key;


            openList.Remove( currentNode );
            closedList.Add( currentNode );


            bool reachEnd = currentNode.Equals( endNode );
            if ( reachEnd )
            {
                return RetracePath( startNode, currentNode, directions ); //gather collect accumulate, congest, agglomerate, gather, congregate RetracePath assemble, accumulate, compile
            }

            NodeValues nodeValues = openList[ currentNode ];
            foreach ( KeyValuePair<T, float> nodeAndDistance in _connectedNodesAndStepCosts( currentNode ) )
            {
                T neighbour = nodeAndDistance.Key;
                float distanceCurrentToNeighbour = nodeAndDistance.Value;

                if ( closedList.Contains<T>( neighbour ) )
                    continue;


                float distanceStartToNeighbour = distanceCurrentToNeighbour + nodeValues.G;

                bool containsKey = openList.ContainsKey( neighbour ) &&
                                   !( openList[ neighbour ].G > (double) distanceStartToNeighbour );

                if ( containsKey )
                    continue;

                float endToNeighbourDistance = _heuristicDistance( neighbour, endNode );
                float finalPriority = endToNeighbourDistance + distanceStartToNeighbour;
                directions[ neighbour ] = currentNode;


                NodeValues values = new NodeValues { G = distanceStartToNeighbour, HeuristicToEnd = endToNeighbourDistance, FinalCost = finalPriority };

                if ( openList.ContainsKey( neighbour ) )
                    openList[ neighbour ] = values;
                else
                    openList.Add( neighbour, values );
            }
        }

        return new Path { };
    }
    static KeyValuePair<T, NodeValues> FindBestPriority( KeyValuePair<T, NodeValues> firstPair, KeyValuePair<T, NodeValues> secondPair )
    {
        KeyValuePair<T, NodeValues> mostSuitable;
        //в двух других также и

        bool secondWayShorter = firstPair.Value.FinalCost >= (double) secondPair.Value.FinalCost;
        // bool sameLength = firstPair.Value.F == (double) secondPair.Value.F;
        // bool secondWayClosestToEnd = firstPair.Value.H > (double) secondPair.Value.H;
        // bool sameLengthAndSecondWayClosestToEnd = sameLength && secondWayClosestToEnd;
        // bool isSecondWayBetterSuitable = secondWayShorter || sameLengthAndSecondWayClosestToEnd;

        if ( secondWayShorter )
            mostSuitable = secondPair;
        else
            mostSuitable = firstPair;

        return mostSuitable;
    }

    Path RetracePath( T startNode, T currentNode, Dictionary<T, T> directions )
    {
        List<T> successfulPath = new List<T>();

        for ( T tracebackStep = currentNode; !tracebackStep.Equals( startNode ); tracebackStep = directions[ tracebackStep ] ) //Трассировка
        {
            successfulPath.Add( tracebackStep );
        }

        successfulPath.Reverse();

        return new Path { IsSucceed = true, Value = successfulPath };
    }


}
}