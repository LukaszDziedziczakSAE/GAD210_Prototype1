using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_MultiplicationSelector : MonoBehaviour
{
    [SerializeField] Button x1_Button;
    [SerializeField] Button x5_Button;
    [SerializeField] Button x10_Button;

    public int Multipler { get; private set; } = 1;

    private void OnEnable()
    {
        x1_Button.onClick.AddListener(OnX1ButtonPress);
        x5_Button.onClick.AddListener(OnX5ButtonPress);
        x10_Button.onClick.AddListener(OnX10ButtonPress);
        UpdateButtonInteractability();
    }

    private void OnDisable()
    {
        x1_Button.onClick.RemoveListener(OnX1ButtonPress);
        x5_Button.onClick.RemoveListener(OnX5ButtonPress);
        x10_Button.onClick.RemoveListener(OnX10ButtonPress);
    }

    private void OnX1ButtonPress()
    {
        Multipler = 1;
        UpdateButtonInteractability();
    }

    private void OnX5ButtonPress()
    {
        Multipler = 5;
        UpdateButtonInteractability();
    }

    private void OnX10ButtonPress()
    {
        Multipler = 10;
        UpdateButtonInteractability();
    }

    private void UpdateButtonInteractability()
    {
        x1_Button.interactable = Multipler != 1;
        x5_Button.interactable = Multipler != 5;
        x10_Button.interactable = Multipler != 10;
    }
}
