using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_BuildMenuItem : MonoBehaviour
{
    [SerializeField] TMP_Text buildingName;
    [SerializeField] GameObject costBox;
    [SerializeField] UI_BuildCostItem buildCostItemPrefab;
    [SerializeField] Button button;

    BuildingConfig buildingConfig;
    List<UI_BuildCostItem> costItems = new List<UI_BuildCostItem>();

    private void OnEnable()
    {
        button.onClick.AddListener(OnButtonPress);
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(OnButtonPress);
    }

    public void Initilise(BuildingConfig config)
    {
        buildingConfig = config;
        buildingName.text = buildingConfig.BuildingName;

        foreach (Resource resource in buildingConfig.BuildCost)
        {
            UI_BuildCostItem buildCostItem = Instantiate(buildCostItemPrefab, costBox.transform);
            costItems.Add(buildCostItem);
            buildCostItem.Initilize(resource);
        }

        button.interactable = buildingConfig.CanAfford;
    }

    public void OnButtonPress()
    {
        print("pressed " + buildingConfig.BuildingName + " build button");
    }
}
