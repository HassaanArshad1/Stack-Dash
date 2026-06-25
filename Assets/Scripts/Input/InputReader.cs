using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    public event Action OnTap;

    private GameInputActions _input;

    private void Awake()
    {
        _input = new GameInputActions();
    }

    private void OnEnable()
    {
        _input.GamePlay.Enable();
        _input.GamePlay.Tap.performed += HandleTap;
    }

    private void OnDisable()
    {
        _input.GamePlay.Tap.performed -= HandleTap;
        _input.GamePlay.Disable();
    }

    private void HandleTap(InputAction.CallbackContext ctx) => OnTap?.Invoke();
}
