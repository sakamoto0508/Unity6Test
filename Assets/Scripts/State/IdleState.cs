using UnityEngine;

public class IdleState : IState
{
    public void OnEnter()
    {
        Debug.Log("Entering Idle State");
    }
    public void OnUpdate()
    {
        // Idle state logic here
    }

    public void OnExit()
    {
        Debug.Log("Exiting Idle State");
    }
}
