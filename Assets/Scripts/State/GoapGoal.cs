using UnityEngine;

public class GoapGoal
{
    public GoapGoal(string name, float priority)
    {
        Name = name;
        Priority = priority;
    }

    public string Name { get; private set; }
    public float Priority {  get; private set; }
}
