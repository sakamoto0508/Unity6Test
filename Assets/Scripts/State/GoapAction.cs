using UnityEngine;

public abstract class GoapAction : MonoBehaviour
{
    public abstract bool IsValid();
    public abstract void OnStart();
    public abstract void OnUpdate();
    public abstract bool IsCompleted();
}
