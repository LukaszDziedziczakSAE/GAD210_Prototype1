using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New BuildingConfig")]
public class BuildingConfig : ScriptableObject
{
    [field: SerializeField] public Building BuildingPrefab {  get; private set; }
    [field: SerializeField] public Resource.EType Type { get; private set; }
    [field: SerializeField] public string BuildingName { get; private set; }
    [field: SerializeField] public Texture Icon { get; private set; }
    [field: SerializeField] public Resource[] BuildCost { get; private set; }

    public bool CanAfford
    {
        get
        {
            foreach (Resource resource in BuildCost)
            {
                if (!TownCenter.Instance.Resources.HasResource(resource)) return false;
            }
            return true;
        }
    }
}
