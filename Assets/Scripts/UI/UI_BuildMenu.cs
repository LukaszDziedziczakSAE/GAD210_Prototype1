using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_BuildMenu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] UI_BuildMenuItem buildMenuItemPrefab;
    [SerializeField] GameObject contentBox;

    List<UI_BuildMenuItem> list = new List<UI_BuildMenuItem>();

    private void OnEnable()
    {
        BuildList();
    }

    private void OnDisable()
    {
        ClearList();
    }

    public void ClearList()
    {
        if (list.Count > 0 )
        {
            foreach(UI_BuildMenuItem item in list)
            {
                Destroy(item.gameObject);
            }
            list.Clear();
        }
    }

    public void BuildList()
    {
        ClearList();

        foreach (BuildingConfig buildingConfig in Player.Instance.Building.Buildables)
        {
            UI_BuildMenuItem buildMenuItem = Instantiate(buildMenuItemPrefab, contentBox.transform);
            list.Add(buildMenuItem);
            buildMenuItem.Initilise(buildingConfig);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Player.Instance.Input.MouseOverUI = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Player.Instance.Input.MouseOverUI = false;
    }
}
