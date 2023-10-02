namespace Field.GridManipulation.Pathfinding
{
struct NodeValues
{
    public float G; //true distance from origin
    public float HeuristicToEnd; //H. heuristic to end. "optimistic", прямой без препятствий //priorityToEnd
    public float FinalCost; //f = g+h //FinalPriority-значило бы что чем выше тем лучше //чем меньше - тем приоритетнее
}
}