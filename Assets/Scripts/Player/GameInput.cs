using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    private PlayerInputActions playerInputActions;

    public event EventHandler OnPlayerAttack;
    public event EventHandler OnDashAction;
    public event EventHandler OnInventoryAction;

    private void Awake()//
    {
        Instance = this;
        playerInputActions = new PlayerInputActions();
        playerInputActions.Enable();

        playerInputActions.Player.Dash.performed += Dash_performed;
        playerInputActions.Player.Inventory.performed += Inventory_performed;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnPlayerAttack?.Invoke(this, EventArgs.Empty);
        }
    }

    private void Dash_performed(InputAction.CallbackContext obj)
    {
        OnDashAction?.Invoke(this, EventArgs.Empty);
    }

    private void Inventory_performed(InputAction.CallbackContext obj)
    {
        OnInventoryAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVector()
    {
        if (playerInputActions == null) return Vector2.zero;
        return playerInputActions.Player.Move.ReadValue<Vector2>();
    }
}