using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownCenter : Location
{
    public static TownCenter Instance;

    [field: SerializeField, Header("Town Center")] public TownCenter_Resources Resources {  get; private set; }
    [field: SerializeField] public SwarmMemberSpawner Spawner { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
}
