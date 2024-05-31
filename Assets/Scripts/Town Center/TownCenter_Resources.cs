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

    public void RemoveResource(Resource resource)
    {
        if (resource.Type == Resource.EType.None) return;

        Resource stockpileResource = SockpileResource(resource.Type);
        if (resource.Amount < stockpileResource.Amount)
        {
            stockpileResource.Remove(resource.Amount);
        }
        else
        {
            Stockpile.Remove(stockpileResource);
        }

        UI.Resources.UpdateResourceList();
    }

    public void RemoveResources(Resource[] resources)
    {
        foreach (Resource resource in resources)
        {
            RemoveResource(resource);
        }
    }
    
    public bool CanAfford(Resource resource)
    {
        if (SockpileResource(resource.Type) == null) return false;
        return SockpileResource(resource.Type).Amount >= resource.Amount;
    }

    
}
