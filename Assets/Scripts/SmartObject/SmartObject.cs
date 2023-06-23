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

    private int itemID;
    public bool CanBeItem => itemID > 0;
    public ItemComponent ItemComponent => (ItemComponent)components[itemID];

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

    public void Drop(Vector3 pos)
    {
        transform.position = pos;
        gameObject.SetActive(true);
        OnObjectStateChange?.Invoke();
    }

    public void Destory()
    {

    }

    protected override void Awake()
    {
        base.Awake();
        itemID = 0;

        List<ActionComponent> actionComp = new List<ActionComponent>(actionComponents);
        for(int i = 0; i < components.Length; ++i)
        {
            EntityComponent comp = components[i];
            ActionComponent[] actionArray = comp.GetComponentActions();
            if (actionArray != null)
                actionComp.AddRange(actionArray);

            if (comp.ID == ComponentIDs.ITEM)
            {
                itemID = i;
            }
        }

        actions = new ActionContainer[actionComp.Count];
        for(int i = 0; i < actionComp.Count; ++i)
        {
            actions[i] = new ActionContainer(this, actionComp[i]);
        }
    }

    public bool TryGetAction(ActionID actionID, out IEntityAction action)
    {
        action = GetAction(actionID);
        return action != null;
    }

    public IEntityAction GetAction(ActionID actionID)
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

    public IEnumerable<IEntityAction> GetActions()
    {
        for(int i = 0; i < actions.Length; ++i)
        {
            yield return actions[i].GetAction();
        }
    }
}
