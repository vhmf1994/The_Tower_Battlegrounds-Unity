using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathfindingGrid : CustomGrid
{
    public static PathfindingGrid Instance;

    [Header("Pathfinding Configuration")]
    [SerializeField] private List<PathNode> path;

    private List<PathNode> openList;
    private List<PathNode> closedList;

    [SerializeField] private PathNode startNode;
    [SerializeField] private PathNode finalNode;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Initialize();
    }

    //[Button]
    private void Initialize()
    {
        SetupGrid();
        path = FindPath();
    }

    public List<PathNode> FindPath()
    {
        return FindPath(this.startNode, this.finalNode);
    }
    public List<PathNode> FindPath(int startX, int startY, int endX, int endY)
    {
        PathNode startNode = GetGridNode(startX, startY) as PathNode;
        PathNode endNode = GetGridNode(endX, endY) as PathNode;

        return FindPath(startNode, endNode);
    }
    public List<PathNode> FindPath(PathNode startNode, PathNode endNode)
    {
        openList = new List<PathNode>() { startNode };
        closedList = new List<PathNode>();

        // reseting all path nodes
        for (int x = 0; x < m_gridSize.x; x++)
        {
            for (int y = 0; y < m_gridSize.y; y++)
            {
                PathNode pathNode = GetGridNode(x, y) as PathNode;

                pathNode.gCost = int.MaxValue;
                pathNode.cameFromNode = null;
            }
        }

        // Initializing start node
        startNode.gCost = 0;
        startNode.hCost = CalculateDistance(startNode, endNode);

        // Cicle
        while (openList.Count > 0)
        {
            PathNode currentNode = GetLowestFCostNode(openList);

            if (currentNode == endNode)
            {
                // Reached final node
                return CalculatePath(endNode);
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach (PathNode neighbourNode in currentNode.neighboursGridNodes)
            {
                if (closedList.Contains(neighbourNode)) continue;

                if (neighbourNode.gridNodeType != PathNodeType.Walkable)
                {
                    closedList.Add(neighbourNode);
                    continue;
                }

                int tentativeGCost = currentNode.gCost = CalculateDistance(currentNode, neighbourNode);

                if (tentativeGCost < neighbourNode.gCost)
                {
                    neighbourNode.cameFromNode = currentNode;

                    neighbourNode.gCost = tentativeGCost;
                    neighbourNode.hCost = CalculateDistance(neighbourNode, endNode);

                    if (!openList.Contains(neighbourNode))
                        openList.Add(neighbourNode);
                }
            }
        }

        // Out of nodes on the openList
        return null;
    }

    private List<PathNode> CalculatePath(PathNode endNode)
    {
        List<PathNode> path = new List<PathNode>();

        path.Add(endNode);

        PathNode currentNode = endNode;

        while (currentNode.cameFromNode != null)
        {
            path.Add(currentNode.cameFromNode);
            currentNode = currentNode.cameFromNode;
        }

        path.Reverse();
        return path;
    }
    private int CalculateDistance(PathNode a, PathNode b)
    {
        int xDistance = (int)Mathf.Abs(a.gridPosition.x - b.gridPosition.x);
        int yDistance = (int)Mathf.Abs(a.gridPosition.y - b.gridPosition.y);

        int remainingDistance = Mathf.Abs(xDistance - yDistance);

        return remainingDistance;
    }

    private PathNode GetLowestFCostNode(List<PathNode> pathNodeList)
    {
        PathNode currentlowestFCostNode = pathNodeList.First();

        for (int i = 0; i < pathNodeList.Count; i++)
        {
            if (pathNodeList[i].fCost < currentlowestFCostNode.fCost ||
               pathNodeList[i].fCost == currentlowestFCostNode.fCost && pathNodeList[i].hCost < currentlowestFCostNode.hCost)
                currentlowestFCostNode = pathNodeList[i];
        }

        return currentlowestFCostNode;
    }

    public List<PathNode> GetPath()
    {
        return path;
    }

    public PathNode GetStartNode()
    {
        return startNode;
    }
    public PathNode GetEndNode()
    {
        return finalNode;
    }
}