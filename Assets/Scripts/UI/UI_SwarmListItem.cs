using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_SwarmListItem : MonoBehaviour
{
    [SerializeField] TMP_Text amount;
    [SerializeField] RawImage icon;
    [SerializeField] List<JobIconPair> jobIcons;

    [System.Serializable]
    public class JobIconPair
    {
        public string JobName;
        public Texture JobIcon;
    }


    public void Initilize(string amountOnJob, string jobName)
    {
        amount.text = amountOnJob;
        icon.texture = IconFromJobName(jobName);
    }

    private Texture IconFromJobName(string jobName)
    {
        foreach (JobIconPair pair in jobIcons)
        {
            if (pair.JobName == jobName) return pair.JobIcon;
        }
        return null;
    }
}
