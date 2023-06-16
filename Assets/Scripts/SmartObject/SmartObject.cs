using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartObject : Entity, ISmartObject
{
    [SerializeField]
    protected ObjectType type;
    public ObjectType Type => type;

    [SerializeField]
    protected ActionComponent[] actionComponents;

    protected ActionContainer[] actions;

    public event System.Action OnObjectStateChange;

    private void OnDestroy()
    {
        foreach (ActionContainer container in actions) container.Clear();
    }

    public void SetToItem()
    {
        gameObject.SetActive(false);
        OnObjectStateChange?.Invoke();
        //OnObjectStateChange = null; everyone who wants to keep listening has to reassign but it is better if most of them won't continue to listen.
    }

    protected override void Awake()
    {
        //base.Awake();
        List<ActionComponent> actionComp = new List<ActionComponent>(actionComponents);
        SmartObjectComponent[] smartObjectComponents = GetComponents<SmartObjectComponent>();
        components = new EntityComponent[smartObjectComponents.Length];
        for(int i = 0; i < smartObjectComponents.Length; ++i)
        {
            actionComp.AddRange(smartObjectComponents[i].GetComponentActions());
            components[i] = smartObjectComponents[i];
        }

        actions = new ActionContainer[actionComp.Count];
        for(int i = 0; i < actionComp.Count; ++i)
        {
            actions[i] = new ActionContainer(this, actionComp[i]);
        }
    }

    public bool TryGetAction(ActionID actionID, out IAction action)
    {
        action = GetAction(actionID);
        return action != null;
    }

    public IAction GetAction(ActionID actionID)
    {
        for (int i = 0; i < actions.Length; ++i)
        {
            if (actions[i].ActionID == actionID)
            {
                return actions[i].GetAction();
            }
        }
        return null;
    }
}
