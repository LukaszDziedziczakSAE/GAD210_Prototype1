using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UI_SelectionIndicator : MonoBehaviour
{

    [SerializeField] RectTransform rectTransform;
    [SerializeField] UnityEngine.UI.Image indicator;

    private void Update()
    {
        SetPosition(Player.Instance.Input.MousePosition);
    }

    public void SetPosition(Vector2 position)
    {
        //print("setting indicator position = " + position);
        rectTransform.anchoredPosition = position;
    }

    public void SetProgress(float progress)
    {
        //print("setting indicator progress = " + progress);
        indicator.fillAmount = progress;
    }
}
