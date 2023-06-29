using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Human human;
    private HumanAI ai;

    private bool hasControl;

    private IEntityAction moveForward;
    private IEntityAction turnRight;
    private IEntityAction turnLeft;
    private IEntityAction attack;

    public IEntityAction action1;
    public IEntityAction action2;
    public IEntityAction action3;

    private void Start()
    {
        ai = human.GetComponent<HumanAI>();
        moveForward = human.GetAction(ActionID.MOVE_FORWARD);
        turnLeft = human.GetAction(ActionID.TURN_LEFT);
        turnRight = human.GetAction(ActionID.TURN_RIGHT);
        attack = human.GetAction(ActionID.ATTACK);
    }

    private void OnTriggerEnter(Collider other)
    {
        SmartObject so = other.gameObject.GetComponent<SmartObject>();
        if (so != null)
        {
            int counter = 0;
            foreach (IEntityAction action in so.GetActions())
            {
                if (action.CanBeExecutedBy(human))
                {
                    switch (counter)
                    {
                        case 0: action1 = action; break;
                        case 1: action2 = action; break;
                        case 2: action3 = action; break;
                        default: return;
                    }
                    ++counter;
                    Debug.Log($"Action {action.Name} can be executed. [{counter}]");
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        ClearActions();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeControl();
        }

        if (hasControl)
        {
            if(Input.GetKeyDown(KeyCode.W))
            {
                human.AddAction(moveForward);
            }
            else if (Input.GetKeyUp(KeyCode.W))
            {
                moveForward.Interrupt();
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                human.AddAction(turnLeft);
            }
            else if (Input.GetKeyUp(KeyCode.A))
            {
                turnLeft.Interrupt();
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                human.AddAction(turnRight);
            }
            else if (Input.GetKeyUp(KeyCode.D))
            {
                turnRight.Interrupt();
            }
            if (Input.GetMouseButtonDown(0))
            {
                human.AddAction(attack);
            }

            if(Input.GetKeyDown(KeyCode.Alpha1) && action1 != null)
            {
                human.AddAction(action1);
                ClearActions();
            }
            if (Input.GetKeyDown(KeyCode.Alpha2) && action1 != null)
            {
                human.AddAction(action2);
                ClearActions();
            }
            if (Input.GetKeyDown(KeyCode.Alpha3) && action1 != null)
            {
                human.AddAction(action3);
                ClearActions();
            }
        }
        #region DEBUG
        else
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                human.DebugActionRingBuffer();
            }
        }
        #endregion
    }

    private void ClearActions()
    {
        action1 = null;
        action2 = null;
        action3 = null;
    }

    private void ChangeControl()
    {
        ai.enabled = hasControl;
        hasControl = !hasControl;
        transform.SetParent(human.transform, false);
    }
}
