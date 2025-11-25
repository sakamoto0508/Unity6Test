using UnityEngine;

public class StateMachine 
{
    private IState _currentState;
    public void ChangeState(IState nextState)
    {
        // 現在のステートのExitを実行。
        _currentState?.Exit();
        // 次のステートに変更。
        _currentState = nextState;
        // 次のステートのEnterを実行。
        _currentState.Enter();
    }

    public void Update()
    {
        _currentState?.Update();
    }
}
