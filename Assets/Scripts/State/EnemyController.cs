using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Transform Player { get; private set; }
    private GoapAgent _goapAgent;
    private StateMachine _stateMachine;
    private NavMeshAgent _navMeshAgent;
    public void MoveTo(Vector3 point)
    {
        _stateMachine.ChangeState(new MoveState(_navMeshAgent, point));
    }

    private void Awake()
    {
        _goapAgent = GetComponent<GoapAgent>();
        _stateMachine = GetComponent<StateMachine>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        _stateMachine.ChangeState(new IdleState());
    }

    private void Update()
    {
        _goapAgent.UpdateAgent();
    } 
}
