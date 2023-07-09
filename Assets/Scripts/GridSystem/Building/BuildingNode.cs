using UnityEngine;

public enum BuildingNodeType
{
    Buildable,
    Not_Buildable,
}

public class BuildingNode : CustomGridNode
{
    [Header("Building Node Configuration")]
    [SerializeField] private Transform currentBuilding;
    [SerializeField] private SpriteRenderer selectableSprite;

    [SerializeField] private BuildingNodeType m_buildingNodeType;

    private void OnValidate()
    {
        selectableSprite = GetComponentInChildren<SpriteRenderer>(true);
    }

    public void SetBuilding(Transform newBuilding)
    {
        currentBuilding = newBuilding;
    }

    public void DemolishBuild()
    {
        currentBuilding = null;
    }

    public void Select()
    {
        selectableSprite.enabled = true;
    }
    public void Deselect()
    {
        selectableSprite.enabled = false;
    }

    public bool IsBuildable()
    {
        return !HasBuild() && m_buildingNodeType == BuildingNodeType.Buildable;
    }
    public bool HasBuild()
    {
        return currentBuilding != null;
    }
}
