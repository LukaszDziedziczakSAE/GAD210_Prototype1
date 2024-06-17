using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Housing : Location
{
    [SerializeField] int housingProvided;

    public void IncreaseSwarmSize()
    {
        TownCenter.Instance.Spawner.SpawnNew(housingProvided);
    }
}
