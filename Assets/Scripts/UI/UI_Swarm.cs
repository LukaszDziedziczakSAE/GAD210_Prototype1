using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Swarm : MonoBehaviour
{
    [SerializeField] UI_SwarmListItem swarmListItemPrefab;
    [SerializeField] TownCenter townCenter;

    List<UI_SwarmListItem> list= new List<UI_SwarmListItem>();

    public void UpdateSwarmList()
    {
        ClearList();

        Dictionary<string, int> tally = new Dictionary<string, int>();

        foreach (SwarmMember swarmMember in Swarm.Members)
        {
            string jobName = swarmMember.Status;
            if (!tally.ContainsKey(jobName))
            {
                tally.Add(jobName, 1);
            }
            else
            {
                tally[jobName]++;
            }
        }

        foreach(KeyValuePair<string, int> keyValuePair in tally)
        {
            UI_SwarmListItem swarmListItem = Instantiate(swarmListItemPrefab, transform);
            swarmListItem.Initilize(keyValuePair.Key + " " + keyValuePair.Value.ToString() + "/" + Swarm.Members.Count.ToString());
            list.Add(swarmListItem);
        }
    }

    private void ClearList()
    {
        if (list.Count > 0)
        {
            foreach (UI_SwarmListItem listItem in list)
            {
                Destroy(listItem.gameObject);
            }
            list.Clear();
        }
    }

}
