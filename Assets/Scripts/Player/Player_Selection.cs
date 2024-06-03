using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

public class Player_Selection : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] float rayCastDistance = 100f;
    [SerializeField] float selectionTime = 0.5f;

    float selectionStartTime;
    float selectionDuration => Time.time - selectionStartTime; 
    float selectionProgress => selectionDuration / selectionTime;

    Location selectedLocation;
    Building selectedBuilding;

    private void OnEnable()
    {
        player.Input.OnMousePress += Input_OnMousePress;
        player.Input.OnMouseRelease += Input_OnMouseRelease;
    }

    private void Update()
    {
        if (UI.SelectionIndicator.gameObject.activeSelf && UI.SelectionIndicator.UsableState)
        {
            UI.SelectionIndicator.SetProgress(selectionProgress);
            if (selectionProgress >= 1)
            {
                //print("process complete");
                selectionStartTime = Time.time;

                if (selectedLocation != null)
                {
                    ResourceProvider resourceProvider = selectedLocation.GetComponent<ResourceProvider>();
                    if (resourceProvider != null)
                    {
                        if (!resourceProvider.CanAfford)
                        {
                            Input_OnMouseRelease();
                            return;
                        }

                        resourceProvider.ConsumeCost();
                    }

                    if (!Swarm.HasIdleMember)
                    {
                        Input_OnMouseRelease();
                        return;
                    }

                    for (int sm = 0; sm < UI.MultiplicationSelector.Multipler; sm++)
                    {
                        SwarmMember swarmMember = Swarm.NextIdle;
                        if (swarmMember != null)
                        {
                            swarmMember.Job.SetCurrentJob(selectedLocation);
                        }
                    }
                }

                else if (selectedBuilding != null)
                {

                }
            }
        }
    }

    private void Input_OnMousePress()
    {
        if (player.Input.MouseOverUI || player.Building.IsPlacing) return;

        Ray ray = player.Camera.ScreenPointToRay(player.Input.MousePosition);
        //Debug.DrawRay(ray.origin, ray.direction * rayCastDistance, Color.red, 10f);
        if (Physics.Raycast(ray, out RaycastHit hit, rayCastDistance, Game.LocationLayer))
        {
            if (hit.collider.TryGetComponent<TownCenter>(out TownCenter townCenter))
            {
                UI.BuildMenu.gameObject.SetActive(true);
            }

            else if (hit.collider.TryGetComponent<Building>(out Building building) &&
                building.State == Building.EState.placed)
            {
                if (Swarm.HasIdleMember)
                {
                    selectionStartTime = Time.time;
                    UI.SelectionIndicator.gameObject.SetActive(true);
                    UI.SelectionIndicator.SetColor(true);

                    selectedBuilding = building;
                }

                else
                {
                    UI.SelectionIndicator.gameObject.SetActive(true);
                    UI.SelectionIndicator.SetColor(false);
                    UI.SelectionIndicator.SetProgress(1);
                }
            }

            else if (hit.collider.TryGetComponent<ResourceProvider>(out ResourceProvider resourceProvider) )
            {
                if (Swarm.HasIdleMember && resourceProvider.CanAfford)
                {
                    selectionStartTime = Time.time;
                    UI.SelectionIndicator.gameObject.SetActive(true);
                    UI.SelectionIndicator.SetColor(true);

                    //Location location = hit.collider.GetComponent<Location>();
                    selectedLocation = resourceProvider;
                }

                else
                {
                    UI.SelectionIndicator.gameObject.SetActive(true);
                    UI.SelectionIndicator.SetColor(false);
                    UI.SelectionIndicator.SetProgress(1);
                }
            }
            
        }

        else if (UI.BuildMenu.gameObject.activeSelf)
        {
            UI.BuildMenu.gameObject.SetActive(false);
        }
    }

    private void Input_OnMouseRelease()
    {
        if (player.Input.MouseOverUI || player.Building.IsPlacing) return;

        if (UI.SelectionIndicator.gameObject.activeSelf)
        {
            UI.SelectionIndicator.gameObject.SetActive(false);
        }
        selectedLocation = null;
        selectedBuilding = null;
    }

    private void OnDisable()
    {
        player.Input.OnMousePress -= Input_OnMousePress;
        player.Input.OnMouseRelease -= Input_OnMouseRelease;
    }
}
