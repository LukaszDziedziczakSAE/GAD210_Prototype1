using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_ResourceListItem : MonoBehaviour
{
    [SerializeField] TMP_Text swarmInfo;

    public void Initilize(string swarmInfoText)
    {
        swarmInfo.text = swarmInfoText;
    }
}
