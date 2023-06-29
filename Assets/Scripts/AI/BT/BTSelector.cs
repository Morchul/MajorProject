using UnityEngine;

public class BTSelector : BTComposite
{
    public BTSelector(string name, params AbstractBTNode[] children) : base(name, children)
    {}

    public override BTStatus Tick()
    {
        foreach (AbstractBTNode node in children)
        {
            BTStatus state = node.Tick();
            Debug.Log($"node {node.Name} finished with state: {state}");
            if (state == BTStatus.SUCCESS || state == BTStatus.RUNNING) return state;
        }
        return BTStatus.FAILURE;
    }
}
