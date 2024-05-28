using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownCenter : Location
{
    [field: SerializeField, Header("Town Center")] public TownCenter_Resources Resources {  get; private set; }
    [field: SerializeField] public SwarmMemberSpawner Spawner { get; private set; }
    
    
}
