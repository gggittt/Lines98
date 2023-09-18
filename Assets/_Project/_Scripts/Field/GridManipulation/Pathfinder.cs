using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


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

public class Pathfinder<TNode>
{

    //https://youtu.be/P7sFfFLH4iM?t=120
    readonly int _calculatorPatience = 9_999;
    readonly Func<TNode, TNode, float> _getHeuristicDistance;
    readonly Func<TNode, Dictionary<TNode, float>> _getConnectedNodesAndStepCosts; //именно connected, т.к. связи мб не только с neighbours
    //пока нужна только: Func<T, T> _walkableNeighbours; //пока stepCost не нужен?

    public Pathfinder( Func<TNode, TNode, float> getHeuristicDistance,
        Func<TNode, Dictionary<TNode, float>> getConnectedNodesAndStepCosts )
    {
        _getHeuristicDistance = getHeuristicDistance;
        _getConnectedNodesAndStepCosts = getConnectedNodesAndStepCosts;
    }

    public Path GenerateAStarPath( TNode startNode, TNode endNode )
    {

        float startToEndDistance = _getHeuristicDistance( startNode, endNode );
        int calculatorPatience = _calculatorPatience;

        HashSet<TNode> closedList = new HashSet<TNode>();

        Dictionary<TNode, NodeValues> openList = new Dictionary<TNode, NodeValues>();
        openList.Add( startNode, new NodeValues { G = 0.0f, HeuristicToEnd = startToEndDistance, FinalCost = startToEndDistance } );

        Dictionary<TNode, TNode> directions = new Dictionary<TNode, TNode>(); //вместо этого - хранить currentNode.ParentNode. но тут node просто T. мб обязать where T : INode -> Parent { get; }   float GetDistance( otherCell );

        while ( calculatorPatience > 0 ) //todo удалить? нет, можно только заменить на openList.Any()
        {
            --calculatorPatience;
            if ( openList.Count == 0 )
                break;

            //T currentNode = openList.Aggregate( ( l, r )=> l.Value.F >= (double) r.Value.F ? r : l ).Key; //нечитаемо

            KeyValuePair<TNode, NodeValues> valuePair = openList.Aggregate( FindBestPriority );
            TNode currentNode = valuePair.Key;


            openList.Remove( currentNode );
            closedList.Add( currentNode );


            bool reachEnd = currentNode.Equals( endNode );
            if ( reachEnd )
            {
                return RetracePath( startNode, currentNode, directions ); //gather collect accumulate, congest, agglomerate, congregate RetracePath assemble, accumulate, compile
            }

            NodeValues nodeValues = openList[ currentNode ];
            foreach ( KeyValuePair<TNode, float> nodeAndDistance in _getConnectedNodesAndStepCosts( currentNode ) )
            {
                TNode neighbour = nodeAndDistance.Key;
                float distanceCurrentToNeighbour = nodeAndDistance.Value;

                if ( closedList.Contains<TNode>( neighbour ) )
                    continue;


                float distanceStartToNeighbour = distanceCurrentToNeighbour + nodeValues.G;

                bool containsKey = openList.ContainsKey( neighbour ) &&
                                   !( openList[ neighbour ].G > (double) distanceStartToNeighbour );

                if ( containsKey )
                    continue;

                float endToNeighbourDistance = _getHeuristicDistance( neighbour, endNode );
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
    static KeyValuePair<TNode, NodeValues> FindBestPriority( KeyValuePair<TNode, NodeValues> firstPair, KeyValuePair<TNode, NodeValues> secondPair )
    {
        KeyValuePair<TNode, NodeValues> mostSuitable;
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

    Path RetracePath( TNode startNode, TNode currentNode, Dictionary<TNode, TNode> directions )
    {
        List<TNode> successfulPath = new List<TNode>();

        for ( TNode tracebackStep = currentNode; !tracebackStep.Equals( startNode ); tracebackStep = directions[ tracebackStep ] ) //Трассировка
        {
            successfulPath.Add( tracebackStep );
        }

        successfulPath.Reverse();

        return new Path { IsSucceed = true, Value = successfulPath };
    }


}
