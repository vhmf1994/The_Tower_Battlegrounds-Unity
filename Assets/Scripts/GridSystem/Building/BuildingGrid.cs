using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuildingGrid : CustomGrid
{
    [Header("Building Configuration")]
    [SerializeField] private BuildingNode selectedNode;

    // Temp
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SelectBuildNode(Utils.GetWorldMousePosition());
        }
    }

    private void Initialize()
    {
        SetupGrid(m_gridNodeSize);
    }

    public void SelectBuildNode(Vector3 worldPosition)
    {
        BuildingNode buildingNode = GetGridNode(worldPosition) as BuildingNode;

        if (selectedNode != null)
        {
            selectedNode.Deselect();
        }

        if (buildingNode == null)
        {
            return;
        }

        if (buildingNode.IsBuildable())
        {
            selectedNode = buildingNode;
            selectedNode.Select();
        }
    }
}
