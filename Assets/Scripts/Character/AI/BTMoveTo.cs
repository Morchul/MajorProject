using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTMoveTo : AbstractBTNode
{

    private readonly IEntityAction moveForward;
    private readonly IEntityAction turnRight;
    private readonly IEntityAction turnLeft;

    private readonly Agent agent;
    private readonly CharacterBaseAI ai;

    public float MaxDistanceSqrt { get; set; }

    public BTMoveTo(Agent entity, CharacterBaseAI ai)
    {
        this.agent = entity;
        this.ai = ai;

        moveForward = agent.GetAction(ActionID.MOVE_FORWARD);
        turnLeft = agent.GetAction(ActionID.TURN_LEFT);
        turnRight = agent.GetAction(ActionID.TURN_RIGHT);
    }

    public override BTStatus Tick()
    {
        if (ai.MoveTarget == Vector3.zero) return BTStatus.FAILURE;

        Vector3 dir = ai.MoveTarget - agent.transform.position;
        float dotProduct = Vector2.Dot(agent.transform.forward.ToVector2_XZ(), dir.ToVector2_XZ().normalized);
        Vector3 crossProduct = Vector3.Cross(agent.transform.forward, dir);

        if (dotProduct < 0.99f)
        {
            if (crossProduct.y > 0)
            {
                agent.AddAction(turnRight);
                turnLeft.Interrupt();
            }
            else
            {
                agent.AddAction(turnLeft);
                turnRight.Interrupt();
            }
        }
        else
        {
            turnRight.Interrupt();
            turnLeft.Interrupt();
        }

        if (dir.sqrMagnitude < MaxDistanceSqrt)
        {
            moveForward.Interrupt();
        }
        else if (dotProduct > 0.7f)
            agent.AddAction(moveForward);
        else
            moveForward.Interrupt();

        if (moveForward.Status == ActionState.INACTIVE && turnRight.Status == ActionState.INACTIVE && turnLeft.Status == ActionState.INACTIVE)
        {
            ai.MoveTarget = Vector3.zero; //Target reached
            return BTStatus.SUCCESS;
        }
        else
            return BTStatus.RUNNING;
    }

    /*private readonly IEntityAction moveForward;
    private readonly IEntityAction turnRight;
    private readonly IEntityAction turnLeft;

    private readonly MoveAction moveAction;

    private readonly Agent entity;

    public float MaxDistanceSqrt { get; set; }

    public Vector3 MoveTarget { get; private set; }
    private readonly NavMeshAgent navMeshAgent;
    private int pathIndex;

    public Transform debugObject;

    public BTMoveTo(Agent entity)
    {
        this.entity = entity;

        navMeshAgent = entity.GetComponent<NavMeshAgent>();
        navMeshAgent.updatePosition = false;
        navMeshAgent.updateRotation = false;

        moveForward = entity.GetAction(ActionID.MOVE_FORWARD);
        turnLeft = entity.GetAction(ActionID.TURN_LEFT);
        turnRight = entity.GetAction(ActionID.TURN_RIGHT);

        moveAction = (MoveAction)entity.GetAction(ActionID.MOVE);

    }

    public void SetTarget(Vector3 targetPos)
    {

        if (targetPos == MoveTarget) return;

        //Debug.Log("Search for path to pos: " + targetPos);
        MoveTarget = targetPos;

        CalcPath();
    }

    private void CalcPath()
    {
        //Debug.Log("Calc path to: " + MoveTarget);
        pathIndex = 1;
        //navMeshAgent.SetDestination(MoveTarget);
        NavMeshPath path = new NavMeshPath();
        if (navMeshAgent.CalculatePath(MoveTarget, path))
        {
            Debug.Log("Path found");
        }
        else
        {
            Debug.Log("Path not found");
        }
        Debug.Log($"Final path with {path.corners.Length} corners");
        for(int i = 0; i < path.corners.Length; ++i)
        {
            Debug.Log($"Corner {i}: {path.corners[i]}");
        }


        //else
        //{
        //    //Debug.Log("Can't reach exactly current path length: " + path.corners.Length);
        //    if (navMeshAgent.FindClosestEdge(out NavMeshHit hit))
        //    {
        //        navMeshAgent.CalculatePath(hit.position, path);
        //    }
        //    else
        //    {
        //        MoveTarget = Vector3.zero;
        //        Debug.Log("Path not found");
        //    }
        //}
    }

    public override BTStatus Tick()
    {
        if (pathIndex == 0) return BTStatus.FAILURE;

        if (navMeshAgent.pathPending)
        {
            Debug.Log("Path pending");
            return BTStatus.RUNNING;
        }

        //if (navMeshAgent.path.corners.Length < 2) return BTStatus.SUCCESS;

        Debug.Log("Result path " + navMeshAgent.path.corners.Length);
        for (int i = 0; i < navMeshAgent.path.corners.Length; ++i)
        {
            Debug.Log($"Corner {i}: {navMeshAgent.path.corners[i]}");
        }

        int index = navMeshAgent.path.corners.Length == 1 ? 0 : 1;

        Vector3 nextTargetPos = navMeshAgent.path.corners[index];
        Vector3 finalTargetPos = navMeshAgent.path.corners[navMeshAgent.path.corners.Length - 1];
        debugObject.position = nextTargetPos;
        //Vector3 targetPos = navMeshAgent.nextPosition;
        //Debug.Log("TargetPos: " + targetPos);

        Vector3 dir = nextTargetPos - entity.transform.position;
        float dotProduct = Vector2.Dot(entity.transform.forward.ToVector2_XZ(), dir.ToVector2_XZ().normalized);
        Vector3 crossProduct = Vector3.Cross(entity.transform.forward, dir);

        
        moveAction.MoveDir = navMeshAgent.desiredVelocity;

        //if(moveAction.MoveDir.sqrMagnitude < 0.1f)
        //{
        //    moveAction.Interrupt();
        //}
        //else
        //{
        //    entity.AddAction(moveAction);
        //}

        //if (dir.sqrMagnitude < 0.1f)
        //{
        //    moveForward.Interrupt();
        //}
        //else if (dotProduct > 0.7f)
        //    entity.AddAction(moveForward);
        //else
        //    moveForward.Interrupt();

        //if (dotProduct < 0.99f && dir.sqrMagnitude > 0.1f)
        //{
        //    if (crossProduct.y > 0)
        //    {
        //        entity.AddAction(turnRight);
        //        turnLeft.Interrupt();
        //    }
        //    else
        //    {
        //        entity.AddAction(turnLeft);
        //        turnRight.Interrupt();
        //    }
        //}
        //else
        //{
        //    turnRight.Interrupt();
        //    turnLeft.Interrupt();
        //}

        if (moveAction.Status == ActionState.INACTIVE &&
            //moveForward.Status == ActionState.INACTIVE && 
            //turnRight.Status == ActionState.INACTIVE && 
            //turnLeft.Status == ActionState.INACTIVE &&
            (finalTargetPos - entity.transform.position).sqrMagnitude < MaxDistanceSqrt)
        {
            return BTStatus.SUCCESS;
        }
        else
            return BTStatus.RUNNING;
    }*/

    public override void CleanUp()
    {
        turnRight.Interrupt();
        turnLeft.Interrupt();
        moveForward.Interrupt();
    }
}
