using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public enum PathNodeType
    {
        Walkable,
        Not_Walkable,
    }

    public class PathNode : CustomGridNode
    {
        [Header("Path Node Configuration")]
        [SerializeField] private int m_gCost;
        [SerializeField] private int m_hCost;

        [SerializeField] private PathNodeType m_pathNodeType;

        private PathNode m_cameFromNode;

        public int gCost { get { return m_gCost; } set { m_gCost = value; } }
        public int hCost { get { return m_hCost; } set { m_hCost = value; } }
        public int fCost { get { return m_gCost + m_hCost; } }

        public PathNodeType gridNodeType { get { return m_pathNodeType; } }
        public PathNode cameFromNode { get { return m_cameFromNode; } set { m_cameFromNode = value; } }
    }