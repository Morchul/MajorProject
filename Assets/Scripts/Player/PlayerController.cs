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

    private void Awake()
    {
        ai = human.GetComponent<HumanAI>();
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
                moveForward = human.GetAction(ActionID.MOVE_FORWARD);
                human.AddAction(moveForward);
            }
            else if (Input.GetKeyUp(KeyCode.W))
            {
                moveForward.Interrupt();
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                turnLeft = human.GetAction(ActionID.TURN_LEFT);
                human.AddAction(turnLeft);
            }
            else if (Input.GetKeyUp(KeyCode.A))
            {
                turnLeft.Interrupt();
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                turnRight = human.GetAction(ActionID.TURN_RIGHT);
                human.AddAction(turnRight);
            }
            else if (Input.GetKeyUp(KeyCode.D))
            {
                turnRight.Interrupt();
            }
            if (Input.GetMouseButtonDown(0))
            {
                human.AddAction(human.GetAction(ActionID.ATTACK));
            }
        }
    }

    private void ChangeControl()
    {
        ai.enabled = hasControl;
        hasControl = !hasControl;
    }
}
