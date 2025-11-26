using UnityEngine;

public abstract class GoapAction : MonoBehaviour
{
    public abstract bool IsValid();
    public abstract void OnStart();
    public abstract void OnUpdate();
    public abstract bool IsCompleted();
    // 優先度（デフォルト0）
    protected float priority = 0f;
    // 優先度を返すメソッド（ChooseAction() が使う）
    public virtual float GetPriority()
    {
        return priority;
    }

    // 便利：サブクラスから優先度を設定できる
    protected void SetPriority(float p) => priority = p;
}
