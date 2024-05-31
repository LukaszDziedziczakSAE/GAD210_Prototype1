using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceProvider : Location
{
    [field: SerializeField, Header("Resource Provider")] public float JobDuration { get; private set; } = 1f;
    [field: SerializeField] public Resource.EType ResourceType { get; private set; }
    [field: SerializeField] public Resource Cost { get; private set; }

    public bool CanAfford
    {
        get
        {
            if (Cost.Type == Resource.EType.None) return true;
            return Player.Instance.Resources.CanAfford(Cost);
        }
    }

    public void ConsumeCost()
    {
        Player.Instance.Resources.RemoveResource(Cost);
    }
}
