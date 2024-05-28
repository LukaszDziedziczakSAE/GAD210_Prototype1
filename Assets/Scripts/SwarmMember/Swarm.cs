using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swarm : MonoBehaviour
{
    public static Swarm Instance;
    [SerializeField] List<SwarmMember> swarmMembers = new List<SwarmMember>();

    public static List<SwarmMember> Members => Instance.swarmMembers;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }


    public static SwarmMember NextIdle
    {
        get
        {
            foreach (SwarmMember swarmMember in Instance.swarmMembers)
            {
                if (swarmMember.Job.IsIdle) return swarmMember;
            }
            return null;
        }
    }

    public static bool HasIdleMember => NextIdle != null;

    public static bool NoOtherMemberGoingToDestination(Vector3 destinaton)
    {
        foreach (SwarmMember swarmMember in Instance.swarmMembers)
        {
            if (swarmMember.Movement.PoistionNearDestination(destinaton)) return false;
        }
        return true;
    }
}
