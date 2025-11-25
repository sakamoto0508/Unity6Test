using UnityEngine;
using UnityEngine.AI;

public class MoveState : IState
{
    public MoveState(NavMeshAgent agent,Vector3 target)
    {
        _agent = agent;
        _target = target;
    }

    public void OnEnter()=> _agent.SetDestination(_target);
    public void OnUpdate() { }
    public void OnExit() { }

    private NavMeshAgent _agent;
    private Vector3 _target;
}
