using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CustomGridNode : MonoBehaviour
{
    [Header("Grid Node Configuration")]
    [SerializeField] protected Vector3 m_worldPosition;
    [Space(10)]
    [SerializeField] protected Vector2Int m_gridPosition;

    [SerializeField] protected List<CustomGridNode> m_neighboursGridNodes;
    public Vector3 worldPosition => m_worldPosition;
    public Vector2Int gridPosition => m_gridPosition;
    public List<CustomGridNode> neighboursGridNodes => m_neighboursGridNodes;

    public void SetUpGridNode(Vector2Int gridPosition)
    {
        m_worldPosition = transform.position;
        m_worldPosition.y = 0;

        m_gridPosition = gridPosition;
    }

    public void SetUpNeighbours(List<CustomGridNode> neighboursGridNodes)
    {
        m_neighboursGridNodes = neighboursGridNodes;
    }

    public override string ToString()
    {
        return gameObject.name;
    }
}