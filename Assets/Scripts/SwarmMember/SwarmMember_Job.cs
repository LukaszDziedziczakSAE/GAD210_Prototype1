using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmMember_Job : MonoBehaviour
{
    [SerializeField] SwarmMember swarmMember;

    //[field: SerializeField] public Location CurrentJobLocation {  get; private set; }
    [field: SerializeField] public EState State { get; private set; }

    public bool IsIdle => State == EState.idle;

    float timer;
    float jobDuration;
    Resource inHand;


    public enum EState
    {
        idle,
        movingToJob,
        working,
        returningHome
    }

    private void Awake()
    {
        if (swarmMember == null) swarmMember = GetComponent<SwarmMember>();
    }

    private void Update()
    {
        if (State == EState.working)
        {
            timer += Time.deltaTime;

            if (timer >= jobDuration)
            {
                inHand = new Resource(swarmMember.Movement.CurrentLocation.GetComponent<ResourceProvider>().ResourceType);
                State = EState.returningHome;
                swarmMember.MoveBackToTownCenter();
                Debug.Log(name + " moving home" + (inHand == null ? "" : (", carrying " + inHand.Amount.ToString() + " " + inHand.Type.ToString())));
            }
        }
    }

    public void SetCurrentJob(Location location)
    {
        State = EState.movingToJob;
        swarmMember.Movement.MoveToNewLocation(location);
        UI.Swarm.UpdateSwarmList();
        Debug.Log(name + " moving to job site");
    }

    public void ArrivedAtJobSite()
    {
        jobDuration = swarmMember.Movement.CurrentLocation.GetComponent<ResourceProvider>().JobDuration;
        timer = 0;
        State = EState.working;
        swarmMember.Movement.MoveToPositionInLocalArea();
        Debug.Log(name + " arrived at job site. jobDuration=" + jobDuration);
    }

    public void ArrivedAtHome()
    {
        Debug.Log(name + " arrived home" + (inHand == null? "" : (", delivered " + inHand.Amount.ToString() + " " + inHand.Type.ToString()) ));
        swarmMember.TownCenter.Resources.Add(inHand);
        inHand = null;
        State = EState.idle;
        swarmMember.Movement.MoveToPositionInLocalArea();
        UI.Swarm.UpdateSwarmList();
    }
}
