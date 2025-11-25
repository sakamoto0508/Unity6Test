using UnityEngine;

public class StateMachine 
{
    public IState _currentState { get; private set; }
    public void ChangeState(IState nextState)
    {
        // 現在のステートのExitを実行。
        _currentState?.OnExit();
        // 次のステートに変更。
        _currentState = nextState;
        // 次のステートのEnterを実行。
        _currentState.OnEnter();
    }

    public void Update()
    {
        _currentState?.OnUpdate();
    }
}
