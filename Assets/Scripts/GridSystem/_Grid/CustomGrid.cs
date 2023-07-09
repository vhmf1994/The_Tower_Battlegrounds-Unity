using Sirenix.Utilities;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class CustomGrid : MonoBehaviour
{
    [Header("Grid Configuration")]
    [SerializeField] protected Vector2Int m_gridSize;
    [SerializeField] protected float m_gridNodeSize = 1f;

    protected CustomGridNode[,] m_gridNodes;

    private Vector3 m_gridPositionOffset;

    protected void SetupGrid(float gridNodeSize = 1f)
    {
        m_gridNodeSize = gridNodeSize;
        List<CustomGridNode> gridNodes = GetAndOrganizeNodes();

        CreateGrid(gridNodes);
    }

    private List<CustomGridNode> GetAndOrganizeNodes()
    {
        List<CustomGridNode> gridNodes = GetComponentsInChildren<CustomGridNode>().ToList();

        // Ordenar a lista temporária com base nas coordenadas X e Z
        gridNodes.Sort((a, b) =>
        {
            if (a.worldPosition.z != b.worldPosition.z)
                return a.worldPosition.z.CompareTo(b.worldPosition.z);
            else
                return a.worldPosition.x.CompareTo(b.worldPosition.x);
        });

        // Atualizar a hierarquia dos filhos no objeto pai
        for (int i = 0; i < gridNodes.Count; i++)
        {
            gridNodes[i].transform.SetSiblingIndex(i);
        }

        return gridNodes;
    }

    private void CreateGrid(List<CustomGridNode> gridNodes)
    {
        // Calcula o deslocamento com relação ao centro do grid
        float gap_x = (m_gridSize.x / 2) * m_gridNodeSize;
        float gap_y = (m_gridSize.y / 2) * m_gridNodeSize;

        m_gridPositionOffset = transform.position + (Vector3.back * gap_y) + (Vector3.left * gap_x);

        // Calcula a posição no grid de cada node da lista
        int minGridPositionX = 0;
        int minGridPositionY = 0;

        int maxGridPositionX = 0;
        int maxGridPositionY = 0;

        for (int i = 0; i < gridNodes.Count; i++)
        {
            CustomGridNode currentNode = gridNodes[i];

            GetXY(currentNode.worldPosition, out int x, out int y);

            Vector2Int currentGridPosition = new Vector2Int(x, y);

            currentNode.SetUpGridNode(currentGridPosition);

            if (currentGridPosition.x < minGridPositionX)
                minGridPositionX = currentGridPosition.x;

            if (currentGridPosition.y < minGridPositionY)
                minGridPositionY = currentGridPosition.y;

            if (currentGridPosition.x > maxGridPositionX)
                maxGridPositionX = currentGridPosition.x;

            if (currentGridPosition.y > maxGridPositionY)
                maxGridPositionY = currentGridPosition.y;
        }

        // Calcula o tamanho total do grid
        int sizeX = Mathf.Abs(minGridPositionX) + Mathf.Abs(maxGridPositionX) + 1;
        int sizeY = Mathf.Abs(minGridPositionY) + Mathf.Abs(maxGridPositionY) + 1;

        m_gridSize = new Vector2Int(sizeX, sizeY);
        m_gridNodes = new CustomGridNode[sizeX, sizeY];

        // Armazena e converte a lista de nodes para o vetor grid de 2 dimensões de acordo com a posição do grid de cada node
        for (int n = 0; n < gridNodes.Count; n++)
        {
            CustomGridNode currentNode = gridNodes[n];

            GetXY(currentNode.worldPosition, out int x, out int y);

            m_gridNodes[x, y] = currentNode;
        }

        // Calcula os vizinhos de cada node
        for (int n = 0; n < gridNodes.Count; n++)
        {
            List<CustomGridNode> neighbourCells = new List<CustomGridNode>();

            CustomGridNode currentNode = gridNodes[n];

            int x = currentNode.gridPosition.x;
            int y = currentNode.gridPosition.y;

            // Left
            if (GridContains(x - 1, y))
                neighbourCells.Add(m_gridNodes[x - 1, y]);

            // Right
            if (GridContains(x + 1, y))
                neighbourCells.Add(m_gridNodes[x + 1, y]);

            // Down
            if (GridContains(x, y - 1))
                neighbourCells.Add(m_gridNodes[x, y - 1]);

            // Up
            if (GridContains(x, y + 1))
                neighbourCells.Add(m_gridNodes[x, y + 1]);

            currentNode.SetUpNeighbours(neighbourCells);
        }
    }

    private bool GridContains(int x, int y)
    {
        return x >= 0 && y >= 0 && x < m_gridSize.x && y < m_gridSize.y && m_gridNodes[x, y] != null;
    }

    public Vector3 GetWorldPosition(float x, float y)
    {
        Vector3 worldPosition = new Vector3(x, 0, y) * m_gridNodeSize + m_gridPositionOffset;

        worldPosition.y = 0;

        return worldPosition;
    }
    public void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition - m_gridPositionOffset).x / m_gridNodeSize);
        y = Mathf.FloorToInt((worldPosition - m_gridPositionOffset).z / m_gridNodeSize);
    }

    public CustomGridNode GetGridNode(int x, int y)
    {
        if (GridContains(x, y))
        {
            return m_gridNodes[x, y];
        }
        else
        {
            return null;
        }
    }
    public CustomGridNode GetGridNode(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GetGridNode(x, y);
    }

    public int GetWidth()
    {
        return m_gridSize.x;
    }
    public int GetHeight()
    {
        return m_gridSize.y;
    }
}