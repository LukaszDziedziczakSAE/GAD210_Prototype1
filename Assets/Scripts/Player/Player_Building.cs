using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Building : MonoBehaviour
{
    [SerializeField] Player player;
    [field: SerializeField] public BuildingConfig[] Buildables {  get; private set; }
    [field: SerializeField] public bool IsPlacing { get; private set; }

    Building placementBuilding;
    BuildingConfig config;


    private void Awake()
    {
        if (player == null) player = GetComponent<Player>();
    }

    public void BeginPlacement(BuildingConfig buildingConfig)
    {
        UI.BuildMenu.gameObject.SetActive(false);
        config = buildingConfig;
        placementBuilding = Instantiate(config.BuildingPrefab, GroundPositionUnderMouse, Quaternion.identity);
        placementBuilding.SetToPlacingState();
        IsPlacing = true;
        player.Input.OnMousePress += OnMousePress;
    }

    public void CompletePlacement()
    {
        if (config.CanAfford)
        {
            TownCenter.Instance.Resources.RemoveResources(config.BuildCost);
            placementBuilding.SetPlacedState();
        }
        else
        {
            Destroy(placementBuilding);
        }

        
        placementBuilding = null;
        config = null;
        IsPlacing = false;

        player.Input.OnMousePress -= OnMousePress;
    }

    public Vector3 GroundPositionUnderMouse
    {
        get
        {
            Ray ray = player.Camera.ScreenPointToRay(player.Input.MousePosition);
            float rayCastDistance = 100f;
            //Debug.DrawRay(ray.origin, ray.direction * rayCastDistance, Color.red, 10f);
            if (Physics.Raycast(ray, out RaycastHit hit, rayCastDistance, Game.TerrainLayer))
            {
                return hit.point;
            }

            Debug.LogError("Building placement raycast fail");
            return Vector3.zero;
        }
    }
    
    private void OnMousePress()
    {
        if (placementBuilding != null && placementBuilding.PlacementGood)
        {
            CompletePlacement();
        }
    }
}
