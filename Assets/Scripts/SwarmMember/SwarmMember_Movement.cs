using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmMember_Movement : MonoBehaviour
{
    [SerializeField] SwarmMember swarmMember;
    [SerializeField] float idleTimeMax = 0.5f;
    [SerializeField] float positionProximity = 0.25f;
    [field: SerializeField] public Location CurrentLocation { get; private set; }
    [field: SerializeField] public Location LastLocation { get; private set; }

    float idleTime;
    public bool IsMoving => swarmMember.NavMeshAgent.velocity.magnitude > 0;
    public bool InProximityToDestination => Vector3.Distance(transform.position, swarmMember.NavMeshAgent.destination) <= positionProximity;
    bool needsDestination;
    Vector3 lastPosition;

    private void Awake()
    {
        if (swarmMember == null) swarmMember = GetComponent<SwarmMember>();
    }

    private void Update()
    {
        if (!needsDestination && !IsMoving && InProximityToDestination)
        {
            if (swarmMember.Job.State == SwarmMember_Job.EState.idle ||
                swarmMember.Job.State == SwarmMember_Job.EState.working ||
                swarmMember.Job.State == SwarmMember_Job.EState.building)
            {
                needsDestination = true;
            }
            else if (swarmMember.Job.State == SwarmMember_Job.EState.movingToJob)
            {
                swarmMember.Job.ArrivedAtJobSite();
            }

            else if (swarmMember.Job.State == SwarmMember_Job.EState.returningHome)
            {
                swarmMember.Job.ArrivedAtHome();
            }
                
        }

        if (needsDestination)
        {
            MoveToPositionInLocalArea();
        }

        if (lastPosition == transform.position)
        {
            idleTime += Time.deltaTime;
            if (idleTime >= idleTimeMax)
            {
                needsDestination = true;
            }
        }
        else idleTime = 0;
        lastPosition = transform.position;
    }

    public void MoveToPositionInLocalArea()
    {
        Vector3 newPosition = swarmMember.Movement.CurrentLocation.LocalAreaPosition;

        if (newPosition != Vector3.zero && Swarm.NoOtherMemberGoingToDestination(newPosition))
        {
            swarmMember.NavMeshAgent.SetDestination(newPosition);
            needsDestination = false;
        }
    }

    public void SetCurrentLocation(Location newLocation)
    {
        LastLocation = CurrentLocation;
        CurrentLocation = newLocation;
    }

    public void MoveToNewLocation(Location newLocation)
    {
        SetCurrentLocation(newLocation);
        needsDestination = true; 
        MoveToPositionInLocalArea();
    }

    public bool PoistionNearDestination(Vector3 position)
    {
        return Vector3.Distance(position, swarmMember.NavMeshAgent.destination) <= (swarmMember.NavMeshAgent.radius * 2);
    }

    public bool CurrentLocationIsIncompleteBuilding
    {
        get
        {
            //Debug.Log("CurrentLocationIsIncompleteBuilding");
            if (CurrentLocation != null)
            {
                Building building = CurrentLocation.GetComponent<Building>();
                //if (building != null) Debug.Log("Is Building");
                if (building != null && building.State == Building.EState.placed)
                {
                    //Debug.Log("is in placed state");
                    return true;
                }
            }
            else Debug.LogWarning("CurrentLocation is NULL");
            return false;
        }
    }
}
