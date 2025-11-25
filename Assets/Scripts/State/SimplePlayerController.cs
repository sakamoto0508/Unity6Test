using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class SimplePlayerController : MonoBehaviour
{
    private InputBuffer _InputBuffer;
    private SimplePlayerMover _PlayerMover;
    private void Awake()
    {
        _InputBuffer = GetComponent<InputBuffer>();
        _PlayerMover = GetComponent<SimplePlayerMover>();
    }

    private void Start()
    {
        _InputBuffer.MoveAction.performed += OnMoveAction;
        _InputBuffer.MoveAction.canceled += OnMoveAction;
    }
    

    private void OnDestroy()
    {
        _InputBuffer.MoveAction.performed -= OnMoveAction;
        _InputBuffer.MoveAction.canceled -= OnMoveAction;
    }

    private void OnMoveAction(InputAction.CallbackContext context)
    {
        
    }
}
