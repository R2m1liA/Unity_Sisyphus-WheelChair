using System;
using Unity.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private static InputManager _instance;
    public static InputManager Instance => _instance;
    
    private void Awake()
    {
        if (Instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    [SerializeField, ReadOnly]
    private Vector2 moveValue;
    public Vector2 MoveValue => moveValue;

    [SerializeField, ReadOnly]
    private bool restartValue;
    public bool RestartValue => restartValue;

    private InputAction _moveAction;
    private InputAction _restartAction;

    private void Start()
    {
        _moveAction = InputSystem.actions.FindAction("Move");
        _restartAction = InputSystem.actions.FindAction("Restart");
    }

    private void Update()
    {
        GetMoveValue();
        GetRestartValue();
    }

    private void GetMoveValue()
    {
        moveValue = _moveAction.ReadValue<Vector2>();
    }

    private void GetRestartValue()
    {
        restartValue = _restartAction.WasPressedThisFrame();
    }
}
