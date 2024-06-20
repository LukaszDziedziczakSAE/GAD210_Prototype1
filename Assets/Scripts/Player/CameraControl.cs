using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraControl : MonoBehaviour
{
    [SerializeField] InputReader input;
    [SerializeField] Camera _camera;
    [SerializeField] float distanceToGround;
    [SerializeField] float angle;
    [Header("Zoom")]
    [SerializeField] float minZoom;
    [SerializeField] float maxZoom;
    [SerializeField] float zoomSpeed;
    [Header("Right-Click Pan")]
    [SerializeField] float panSpeed;
    [SerializeField] float panDelay = 0.5f;
    [Header("Edge Pan")]
    [SerializeField] float edgePanSpeed;
    [SerializeField] float edgePanDelay = 0.1f;
    [SerializeField] float edgeSize = 20f;

    bool paning;
    float deselectStartTime;
    float deselectDuration => Time.time - deselectStartTime;
    bool edgePaning;
    float edgePanStartTime;
    float edgePanDuration => Time.time - edgePanStartTime;

    private void OnEnable()
    {
        input.OnMouseSecondaryPress += Input_OnMouseSecondaryPress;
        input.OnMouseSecondaryRelease += Input_OnMouseSecondaryRelease;
    }

    private void OnDisable()
    {
        input.OnMouseSecondaryPress -= Input_OnMouseSecondaryPress;
        input.OnMouseSecondaryRelease -= Input_OnMouseSecondaryRelease;
    }


    private void Update()
    {
        EdgePaning();
        Paning();
        SetDistanceToGround();
        SetCameraPosition();
    }

    private void EdgePaning()
    {
        if (!edgePaning && mouseOnEdge)
        {
            edgePaning = true;
            edgePanStartTime = Time.time;
        }
        else if (edgePaning && !mouseOnEdge)
        {
            edgePaning = false;
        }

        if (edgePaning && edgePanDuration > edgePanDelay)
        {
            Vector3 position = transform.position;

            if (input.MousePosition.x < 0 + edgeSize)
            {
                position.x -= edgePanSpeed * Time.deltaTime;
                //position.z += -input.MouseDelta.y * panSpeed * Time.deltaTime;
            }
            else if (input.MousePosition.x > Screen.width - edgeSize)
            {
                position.x += edgePanSpeed * Time.deltaTime;
            }

            if (input.MousePosition.y < 0 + edgeSize)
            {
                position.z -= edgePanSpeed * Time.deltaTime;
            }
            else if (input.MousePosition.y > Screen.height - edgeSize)
            {
                position.z += edgePanSpeed * Time.deltaTime;
            }

            transform.position = position;
        }
        
        
    }

    private bool mouseOnEdge
    {
        get
        {
            return (input.MousePosition.x < 0 + edgeSize) || (input.MousePosition.x > Screen.width - edgeSize)
                || (input.MousePosition.y < 0 + edgeSize) || (input.MousePosition.y > Screen.height - edgeSize);
        }
    }

    private void Paning()
    {
        if (!paning || deselectDuration < panDelay) return;

        Vector3 position = transform.position;
        position.x += -input.MouseDelta.x * panSpeed * Time.deltaTime;
        position.z += -input.MouseDelta.y * panSpeed * Time.deltaTime;
        transform.position = position;
    }

    private void SetDistanceToGround()
    {
        if (input.Zoom != 0)
        {
            distanceToGround += -input.Zoom * zoomSpeed * Time.deltaTime;
        }


        if (distanceToGround > maxZoom) distanceToGround = maxZoom;
        else if (distanceToGround < minZoom) distanceToGround = minZoom;
    }

    private void SetCameraPosition()
    {
        float y = Mathf.Sin(angle) * distanceToGround;
        float z = Mathf.Cos(angle) * distanceToGround;
        _camera.transform.localPosition = new Vector3(0, y, z);
        _camera.transform.localEulerAngles = new Vector3(angle, 0, 0);
    }

    private void Input_OnMouseSecondaryPress()
    {
        paning = true;
        deselectStartTime = Time.time;
    }

    private void Input_OnMouseSecondaryRelease()
    {
        paning = false;
    }
}
