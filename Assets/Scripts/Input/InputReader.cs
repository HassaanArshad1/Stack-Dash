using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    public event Action OnTap;
    public event Action<Vector2> OnMove;

    private GameInputActions _input;

    private void Awake()
    {
        _input = new GameInputActions();
    }

    private void OnEnable()
    {
        _input.GamePlay.Enable();
        _input.GamePlay.Tap.performed += HandleTap;
        _input.GamePlay.Move.performed += HandleMove;
        _input.GamePlay.Move.canceled += HandleMove;
    }

    private void OnDisable()
    {
        _input.GamePlay.Tap.performed -= HandleTap;
        _input.GamePlay.Move.performed -= HandleMove;
        _input.GamePlay.Move.canceled -= HandleMove;
        _input.GamePlay.Disable();
    }

    private void HandleMove(InputAction.CallbackContext ctx) => 
        OnMove?.Invoke(ctx.ReadValue<Vector2>());

    private void HandleTap(InputAction.CallbackContext ctx) => OnTap?.Invoke();
}
