using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SwarmMember : MonoBehaviour
{
    [field: SerializeField] public NavMeshAgent NavMeshAgent {  get; private set; }
    [field: SerializeField] public CapsuleCollider Collider { get; private set; }
    [field: SerializeField] public SwarmMember_Movement Movement { get; private set; }
    [field: SerializeField] public SwarmMember_Job Job { get; private set; }
    [field: SerializeField] public TownCenter TownCenter { get; private set; }

    private void Awake()
    {
        if (NavMeshAgent == null) NavMeshAgent = GetComponent<NavMeshAgent>();
        if (Collider == null) Collider = GetComponent<CapsuleCollider>();
        if (Movement == null) Movement = GetComponent<SwarmMember_Movement>();
        if (Job == null) Job = GetComponent<SwarmMember_Job>();
    }

    public void Initilise(TownCenter townCenter)
    {
        transform.parent = Swarm.Instance.transform;
        this.TownCenter = townCenter;
        Movement.SetCurrentLocation(townCenter);
        name = "SwarmMember" + (Swarm.Members.Count).ToString("D3");
    }

    public void MoveBackToTownCenter()
    {
        Movement.MoveToNewLocation(TownCenter);
    }

    public string Status
    {
        get
        {
            switch (Job.State)
            {
                case SwarmMember_Job.EState.idle:
                    return "Idle";

                case SwarmMember_Job.EState.movingToJob:
                    if (Movement.CurrentLocationIsIncompleteBuilding) return "Building Constructor";
                    else return Movement.CurrentLocation.JobName;

                case SwarmMember_Job.EState.movingToBuilding:
                    return "Building Constructor";

                case SwarmMember_Job.EState.working:
                    return Movement.CurrentLocation.JobName;

                case SwarmMember_Job.EState.building:
                    return "Building Constructor";

                case SwarmMember_Job.EState.returningHome:
                    return Movement.LastLocation.JobName;

                case SwarmMember_Job.EState.returningHomeFromBuilding:
                    return "Building Constructor";

                default:
                    return "NULL";
            }
        }
    }
}
