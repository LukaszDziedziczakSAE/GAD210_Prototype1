using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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

    private void OnEnable()
    {
        player.Input.OnMousePress += Input_OnMousePress;
        player.Input.OnMouseRelease += Input_OnMouseRelease;
    }

    private void Update()
    {
        if (UI.SelectionIndicator.gameObject.activeSelf)
        {
            UI.SelectionIndicator.SetProgress(selectionProgress);
            if (selectionProgress >= 1)
            {
                //print("process complete");
                selectionStartTime = Time.time;

                SwarmMember swarmMember = Swarm.NextIdle;
                if (swarmMember != null)
                {
                    swarmMember.Job.SetCurrentJob(selectedLocation);
                }

                if (!Swarm.HasIdleMember) Input_OnMouseRelease();
            }
        }
    }

    private void Input_OnMousePress()
    {
        if (!Swarm.HasIdleMember) return;

        Ray ray = player.Camera.ScreenPointToRay(player.Input.MousePosition);
        //Debug.DrawRay(ray.origin, ray.direction * rayCastDistance, Color.red, 10f);
        if (Physics.Raycast(ray, out RaycastHit hit, rayCastDistance, Game.LocationLayer))
        {
            selectionStartTime = Time.time;
            UI.SelectionIndicator.gameObject.SetActive(true);

            Location location = hit.collider.GetComponent<Location>();
            selectedLocation = location;
        }
    }

    private void Input_OnMouseRelease()
    {
        if (UI.SelectionIndicator.gameObject.activeSelf)
        {
            UI.SelectionIndicator.gameObject.SetActive(false);
        }
        selectedLocation = null;
    }

    private void OnDisable()
    {
        player.Input.OnMousePress -= Input_OnMousePress;
        player.Input.OnMouseRelease -= Input_OnMouseRelease;
    }
}
