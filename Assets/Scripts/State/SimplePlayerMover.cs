using UnityEngine;

public class SimplePlayerMover
{
    public SimplePlayerMover(Rigidbody rb, Transform cameraPos)
    {
        _rb = rb;
        _cameraPos = cameraPos;
    }
    private float _moveSpeed = 5f;
    private Transform _cameraPos;
    private Rigidbody _rb;
    private Quaternion _targetRot = Quaternion.identity;
    private Vector3 _moveDirection;
    private Vector2 _moveInput;

    public void SetMoveInput(Vector2 input)
    {
        _moveInput = input;
    }

    public void MoveStop()
    {
        _moveInput = Vector2.zero;
        _moveDirection = Vector3.zero;
        _rb.linearVelocity = Vector3.zero;
    }

    public void Update()
    {
        Move();
        Rotate();
    }

    public void FixedUpdate()
    {
        _rb.linearVelocity = _moveDirection * _moveSpeed * Time.deltaTime;
        _rb.rotation = Quaternion.Slerp(_rb.rotation, _targetRot, 0.1f);
    }

    private void Move()
    {
        // “ü—Í‚ª‚È‚¢‚È‚ç“®‚©‚È‚¢
        if (_moveInput.sqrMagnitude < 0.01f) return;

        // ƒJƒƒ‰‚ÌŒü‚«‚ðŠî€‚ÉˆÚ“®•ûŒü‚ðŒˆ’è
        Vector3 forward = _cameraPos.forward;
        Vector3 right = _cameraPos.right;
        // …•½•ûŒü‚Ì‚Ý‚É§ŒÀ
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();
        _moveDirection = forward * _moveInput.y + right * _moveInput.x;
    }

    private void Rotate()
    {
        if (_moveDirection.sqrMagnitude < 0.01f) return;
        // ˆÚ“®•ûŒü‚ðŒü‚­‚æ‚¤‚É‰ñ“]‚ðÝ’èB
        _targetRot = Quaternion.LookRotation(_moveDirection);
    }
}
