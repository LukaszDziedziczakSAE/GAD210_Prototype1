using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, Controls.IPlayerActions
{
    Controls controls;

    [field: SerializeField] public Vector2 MousePosition;
    [field: SerializeField] public bool MouseClickDown;

    public event Action OnMousePress;
    public event Action OnMouseRelease;
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
}
