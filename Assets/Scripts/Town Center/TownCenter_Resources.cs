using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownCenter_Resources : MonoBehaviour
{
    [field: SerializeField] public List<Resource> Stockpile = new List<Resource>();

    public void Add(Resource resource)
    {
        if (Stockpile.Contains(resource))
        {
            SockpileResource(resource.Type).Add(resource.Amount);
        }
        else
        {
            Stockpile.Add(resource);
        }

        UI.Resources.UpdateResourceList();
    }

    public Resource SockpileResource(Resource.EType resourceType)
    {
        foreach (Resource resource in Stockpile)
        {
            if (resource.Type == resourceType) return resource;
        }
        return null;
    }

    public bool HasResource(Resource resource)
    {
        if (SockpileResource(resource.Type) == null) return false;
        return SockpileResource(resource.Type).Amount >= resource.Amount;
    }
}
