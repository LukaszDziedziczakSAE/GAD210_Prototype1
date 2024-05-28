using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_BuildCostItem : MonoBehaviour
{
    TMP_Text text;

    public void Initilize(Resource resource)
    {
        text = GetComponent<TMP_Text>();
        text.text = resource.Amount.ToString() + " " + resource.Type.ToString();
    }
}
