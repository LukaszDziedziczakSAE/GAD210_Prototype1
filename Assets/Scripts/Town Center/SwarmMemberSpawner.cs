using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmMemberSpawner : MonoBehaviour
{
    [SerializeField] TownCenter townCenter;
    [SerializeField] SwarmMember swarmMemberPrefab;
    [SerializeField] int startingSwarmSize = 10;

    int membersToSpawn = 0;

    private void Awake()
    {
        if (townCenter == null) townCenter = GetComponent<TownCenter>();
    }

    private void Start()
    {
        membersToSpawn = startingSwarmSize;
    }

    private void Update()
    {
        if (membersToSpawn > 0)
        {
            SpawnSwarmMember();
            
        }
    }

    private void SpawnSwarmMember()
    {
        Vector3 spawnPosition = townCenter.LocalAreaPosition;
        if (spawnPosition == Vector3.zero)
        {
            Debug.LogWarning("Failed to generate spawn point");
            return;
        }

        SwarmMember swarmMember = Instantiate(swarmMemberPrefab, spawnPosition, Quaternion.identity);
        Swarm.Members.Add(swarmMember);
        swarmMember.Initilise(townCenter);
        membersToSpawn--;
        UI.Swarm.UpdateSwarmList();
    }

}
