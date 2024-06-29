using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Resources : MonoBehaviour
{
    [SerializeField] UI_ResourceListItem resourceListItemPrefab;
    [SerializeField] TownCenter_Resources townCenter_Resources;

    List<UI_ResourceListItem> list = new List<UI_ResourceListItem>();

    private void Start()
    {
        UpdateResourceList();
    }

    public void UpdateResourceList()
    {
        ClearList();

        foreach (Resource resource in townCenter_Resources.Stockpile)
        {
            UI_ResourceListItem resourceListItem = Instantiate(resourceListItemPrefab, transform);
            string resourceName = resource.Type.ToString();
            if (resourceName == "WoodPlanks") resourceName = "Wood Planks";
            else if (resourceName == "IronOre") resourceName = "Iron Ore";
            resourceListItem.Initilize(resource.Amount.ToString(), UI.ResourceIcon(resource.Type));
            list.Add(resourceListItem);
        }
    }

    private void ClearList()
    {
        if (list.Count > 0)
        {
            foreach (UI_ResourceListItem resourceListItem in list)
            {
                Destroy(resourceListItem.gameObject);
            }
            list.Clear();
        }
    }

}
