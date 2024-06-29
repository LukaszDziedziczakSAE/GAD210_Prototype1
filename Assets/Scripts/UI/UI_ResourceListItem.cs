using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_ResourceListItem : MonoBehaviour
{
    [SerializeField] TMP_Text stockpileAmount;
    [SerializeField] RawImage icon;

    public void Initilize(string stockpileAmount, Texture iconTexture)
    {
        this.stockpileAmount.text = stockpileAmount;
        if (iconTexture != null) icon.texture = iconTexture;
    }
}
