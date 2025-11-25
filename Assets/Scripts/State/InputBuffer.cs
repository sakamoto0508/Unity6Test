using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputBuffer : MonoBehaviour
{
    private const string MOVE_ACTION = "Move";
    public InputAction MoveAction => _moveAction;
    private InputAction _moveAction;

    private void Awake()
    {
        var playerInput = GetComponent<PlayerInput>();
        _moveAction = playerInput.actions[MOVE_ACTION];
    }
}
