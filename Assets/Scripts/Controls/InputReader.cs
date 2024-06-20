using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, Controls.IPlayerActions
{
    Controls controls;

    [field: SerializeField] public Vector2 MousePosition;
    [field: SerializeField] public Vector2 MouseDelta;
    [field: SerializeField] public float Zoom;
    [field: SerializeField] public bool MouseClickDown;
    [field: SerializeField] public bool MouseSecondaryClickDown;

    public event Action OnMousePress;
    public event Action OnMouseRelease;
    public event Action OnMouseSecondaryPress;
    public event Action OnMouseSecondaryRelease;
    public bool MouseOverUI;

    private void Awake()
    {
        controls = new Controls();
        controls.Player.AddCallbacks(this);
        controls.Player.Enable();
    }


    public void OnMouseClick(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            MouseClickDown = true;
            OnMousePress?.Invoke();
        }
        else if (context.canceled)
        {
            MouseClickDown = false;
            OnMouseRelease?.Invoke();
        }
    }

    public void OnMousePosition(InputAction.CallbackContext context)
    {
        MousePosition = context.ReadValue<Vector2>();
    }

    public void OnMouseDelta(InputAction.CallbackContext context)
    {
        MouseDelta = context.ReadValue<Vector2>();
    }

    public void OnZoom(InputAction.CallbackContext context)
    {
        Zoom = context.ReadValue<float>();
    }

    public void OnMouseSecondaryClick(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            MouseSecondaryClickDown = true;
            OnMouseSecondaryPress?.Invoke();
        }
        else if (context.canceled)
        {
            MouseSecondaryClickDown = false;
            OnMouseSecondaryRelease?.Invoke();
        }
    }
}
