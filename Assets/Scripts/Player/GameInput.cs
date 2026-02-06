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

    private void Awake()
    {
        Instance = this;
        playerInputActions = new PlayerInputActions();
        playerInputActions.Enable();

        playerInputActions.Combat.Attack.started += PlayerAttack_started;
        playerInputActions.Player.Dash.performed += Dash_performed;
        playerInputActions.Player.Inventory.performed += Inventory_performed;
    }

    private void PlayerAttack_started(InputAction.CallbackContext obj)
    {
        OnPlayerAttack?.Invoke(this, EventArgs.Empty);
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
        return playerInputActions.Player.Move.ReadValue<Vector2>();
    }
}