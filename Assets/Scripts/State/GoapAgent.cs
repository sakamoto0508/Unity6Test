using System.Linq;
using UnityEngine;

public class GoapAgent : MonoBehaviour
{
    private GoapAction[] _actions;
    private GoapAction _currentAction;

    private void Awake()
    {
        _actions = GetComponents<GoapAction>();
    }

    public void UpdateAgent()
    {
        if (_currentAction != null || _currentAction.IsCompleted())
        {
            _currentAction = ChooseAction();
            if (_currentAction != null)
            {
                _currentAction.OnStart();
            }
        }
        _currentAction?.OnUpdate();
    }

    private GoapAction ChooseAction()
    {
        return _actions
            .Where(a => a.IsValid())
            .OrderByDescending(a => a.GetPriority())
            .FirstOrDefault();
    }
}
