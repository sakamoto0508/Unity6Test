using UnityEngine;

public class WanderAction : GoapAction
{
    [SerializeField] private EnemyController _enemy;
    [SerializeField] private float _wanderDistance = 5f;
    public override bool IsValid()=> true;
    public override float GetPriority() => 1f;
    public override void OnStart()
    {
        Vector3 random = Random.insideUnitSphere * _wanderDistance;
        random.y = 0;
        Vector3 target = transform.position + random;
        _enemy.MoveTo(target);
    }
    public override void OnUpdate() { }
    public override bool IsCompleted() => true;
}
