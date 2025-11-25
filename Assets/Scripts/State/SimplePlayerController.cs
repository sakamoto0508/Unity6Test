using System;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody))]
public class SimplePlayerController : MonoBehaviour
{
    [SerializeField] private InputBuffer _inputBuffer;
    [SerializeField] private SimplePlayerMover _playerMover;
    private Rigidbody _rb;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _playerMover =new SimplePlayerMover(_rb,Camera.main.transform);
    }

    private void Start()
    {
        _inputBuffer.MoveAction.performed += OnMoveAction;
        _inputBuffer.MoveAction.canceled += OnMoveAction;
    }
    

    private void OnDestroy()
    {
        _inputBuffer.MoveAction.performed -= OnMoveAction;
        _inputBuffer.MoveAction.canceled -= OnMoveAction;
    }

    private void Update()
    {
        _playerMover.Update();
    }

    private void FixedUpdate()
    {
        _playerMover.FixedUpdate();
    }

    private void OnMoveAction(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        if (context.performed)
        {
            _playerMover.SetMoveInput(input);
        }
        else if (context.canceled)
        {
            _playerMover.MoveStop();
        }
    }
}
