using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionContainer
{
    public ActionID ActionID => factory.ActionID;

    private readonly ActionFactory factory;
    private readonly Entity entity;

    private readonly List<IAction> localPool;

    public int MaxLocalPoolActions { get; set; }

    public ActionContainer(Entity entity, ActionComponent actionComponent)
    {
        if (actionComponent.StartContainerSize > actionComponent.MaxContainerSize) throw new System.ArgumentException("maxSavedActions has to be bigger or equal as startActions!");

        this.entity = entity;
        factory = GlobalActionFactory.GetFactory(actionComponent.ActionID);

        localPool = new List<IAction>(actionComponent.StartContainerSize);

        MaxLocalPoolActions = actionComponent.MaxContainerSize;
        for (int i = 0; i < actionComponent.StartContainerSize; ++i)
        {
            IAction action = factory.GetAction(false);
            action.Init(entity);
            localPool.Add(action);
        }
    }

    public IAction GetAction()
    {
        foreach(IAction localAction in localPool)
            if (localAction.Status == ActionState.INACTIVE)
                return localAction;

        bool directReturn = localPool.Count == MaxLocalPoolActions;
        IAction action = factory.GetAction(directReturn);
        action.Init(entity);
        if (!directReturn)
            localPool.Add(action);
        return action;

    }

    public void Clear()
    {
        foreach (IAction action in localPool)
            factory.ReturnAction(action);
        localPool.Clear();
    }
}
