using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class Pathfinder<TNode>
{
    readonly int _calculatorPatience = 9_999;
    readonly Func<TNode, TNode, float> _getHeuristicDistance;
    readonly Func<TNode, List<TNode>> _getConnectedNodes; //именно connected, т.к. связи мб не только с neighbours
    int _sideCost;

    public Pathfinder( Func<TNode, TNode, float> getHeuristicDistance,
        Func<TNode, List<TNode>> getConnectedNodes )
    {
        _getHeuristicDistance = getHeuristicDistance;
        _getConnectedNodes = getConnectedNodes;
    }

    public Path<TNode> GenerateAStarPath( TNode startNode, TNode endNode )
    {

        float startToEndDistance = _getHeuristicDistance( startNode, endNode );
        int calculatorPatience = _calculatorPatience;

        HashSet<TNode> closedList = new HashSet<TNode>();

        Dictionary<TNode, NodeValues> openList = new Dictionary<TNode, NodeValues>();
        openList.Add( startNode, new NodeValues
        { G = 0.0f, HeuristicToEnd = startToEndDistance, FinalCost = startToEndDistance } );

        Dictionary<TNode, TNode> directions = new Dictionary<TNode, TNode>(); //вместо этого - хранить currentNode.ParentNode. но тут node просто T. мб обязать where T : INode -> Parent { get; }   float GetDistance( otherCell );

        while ( calculatorPatience > 0 ) //todo удалить? нет, можно только заменить на openList.Any()
        {
            --calculatorPatience;
            if ( openList.Count == 0 )
                break;

            // TNode currentNode = openList.Aggregate( ( l, r )=> l.Value.FinalCost >= (double) r.Value.FinalCost ? r : l ).Key; //нечитаемо

            KeyValuePair<TNode, NodeValues> valuePair = openList.Aggregate( FindBestPriority );
            // KeyValuePair<TNode, NodeValues> valuePair = openList.Aggregate( FindBestPriority );
            // KeyValuePair<TNode, NodeValues> valuePair = FindBestPriority( openList );
            TNode currentNode = valuePair.Key;

            NodeValues nodeValues = openList[ currentNode ];
            openList.Remove( currentNode );
            closedList.Add( currentNode );


            bool reachEnd = currentNode.Equals( endNode );
            if ( reachEnd )
            {
                return RetracePath( startNode, currentNode, directions );
            }

            List<TNode> connected = _getConnectedNodes( currentNode );

            foreach ( TNode neighbour in connected )
            {
                if ( closedList.Contains<TNode>( neighbour ) )
                    continue;

                _sideCost = 1;
                float distanceStartToNeighbour = nodeValues.G + _sideCost;

                bool containsKey = openList.ContainsKey( neighbour ) &&
                                   !( openList[ neighbour ].G > (double) distanceStartToNeighbour );

                if ( containsKey )
                    continue;

                float endToNeighbourDistance = _getHeuristicDistance( neighbour, endNode );
                float finalPriority = endToNeighbourDistance + distanceStartToNeighbour;
                directions[ neighbour ] = currentNode;


                NodeValues values = new NodeValues
                { G = distanceStartToNeighbour, HeuristicToEnd = endToNeighbourDistance, FinalCost = finalPriority };

                if ( openList.ContainsKey( neighbour ) )
                    openList[ neighbour ] = values;
                else
                    openList.Add( neighbour, values );
            }
        }

        return new Path<TNode>
        { };
    }


    static KeyValuePair<TNode, NodeValues> FindBestPriority( KeyValuePair<TNode, NodeValues> firstPair, KeyValuePair<TNode, NodeValues> secondPair )
    {
        KeyValuePair<TNode, NodeValues> mostSuitable;
        //в двух других также и

        bool isAnotherWayShorter = firstPair.Value.FinalCost >= (double) secondPair.Value.FinalCost;
        // bool sameLength = firstPair.Value.F == (double) secondPair.Value.F;
        // bool secondWayClosestToEnd = firstPair.Value.H > (double) secondPair.Value.H;
        // bool sameLengthAndSecondWayClosestToEnd = sameLength && secondWayClosestToEnd;
        // bool isSecondWayBetterSuitable = secondWayShorter || sameLengthAndSecondWayClosestToEnd;

        if ( isAnotherWayShorter )
            mostSuitable = secondPair;
        else
            mostSuitable = firstPair;

        return mostSuitable;
    }

    Path<TNode> RetracePath( TNode startNode, TNode currentNode, Dictionary<TNode, TNode> directions )
    {
        List<TNode> successfulPath = new List<TNode>();

        for ( TNode tracebackStep = currentNode; !tracebackStep.Equals( startNode ); tracebackStep = directions[ tracebackStep ] ) //Трассировка
        {
            successfulPath.Add( tracebackStep );
        }

        successfulPath.Reverse();

        return new Path<TNode>
        { IsSucceed = true, PathData = successfulPath };
    }


}