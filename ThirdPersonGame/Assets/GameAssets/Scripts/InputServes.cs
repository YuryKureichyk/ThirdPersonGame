using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputServes : MonoBehaviour
{
    private  InputSystem_Actions _actions;

    public event Action Jump;
    
    public Vector2 MoveDirection => _actions.Player.Move.ReadValue<Vector2>();

    private void Awake()
    {
        _actions = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        _actions.Player.Jump.performed += OnJump;
        _actions.Enable();
    }


    private void OnDisable()
    {
        _actions.Disable();
        _actions.Player.Jump.performed += OnJump;
    }
    private void OnJump(InputAction.CallbackContext obj)
    {
        Jump?.Invoke();
    }
}
