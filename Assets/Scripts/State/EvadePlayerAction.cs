using UnityEngine;

public class EvadePlayerAction : GoapAction
{
    [SerializeField] EnemyController _enemy;
    private float _safeDistance = 7f;

    public override bool IsValid()
    {
        float dist = Vector3.Distance(transform.position, _enemy.Player.position);
        return dist < _safeDistance;
    }

    public float GetPriority() => 2f;

    public override void OnStart()
    {
        // ƒvƒŒƒCƒ„[‚Ì‹t•ûŒü‚Ö“¦‚°‚é
        Vector3 dir = (transform.position - _enemy.Player.position).normalized;
        Vector3 target = transform.position + dir * 5f;
        _enemy.MoveTo(target);
    }

    public override void OnUpdate() { }
    public override bool IsCompleted() => true;
}
