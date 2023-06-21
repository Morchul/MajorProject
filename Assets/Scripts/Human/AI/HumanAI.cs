using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanAI : AI
{
    [Header("Components")]
    [SerializeField]
    private HungerComponent hunger;
    [SerializeField]
    private CarryComponent carry;

    private Human agent;

    [Header("Blackboard")]
    private SmartObject TargetObject;
    //public Food TargetFood;
    public SmartObject FoodContainer;

    public Vector3 MoveTarget;

    protected override void Start()
    {
        agent = GetComponent<Human>();

        DecisionTransaction hungry = new DecisionTransaction(this, () => hunger.Food < 40, () => hunger.Food > 40);

        BTMoveTo moveTo = new BTMoveTo(agent, this);
        moveTo.MaxDistanceSqrt = 4;

        static AbstractBTNode.BTStatus SuccessToRunning(AbstractBTNode.BTStatus status)
        {
            if (status == AbstractBTNode.BTStatus.SUCCESS) return AbstractBTNode.BTStatus.RUNNING;
            return status;
        }

        BTNode takeOutFromStorage = new BTNode(() =>
        {
            if (FoodContainer.TryGetAction(ActionID.TAKE_OUT, out IEntityAction action))
            {
                agent.AddAction(action);
                return AbstractBTNode.BTStatus.SUCCESS;
            }
            return AbstractBTNode.BTStatus.FAILURE;
        });

        BTNode selectFoodContainer = new BTNode(() =>
        {
            if(FoodContainer == null)
            {
                FoodContainer = BTFindSomething.SearchClosest<SmartObject>("FoodContainer", transform.position, 10);
            }
            
            if(FoodContainer != null)
            {
                MoveTarget = FoodContainer.transform.position;
                return AbstractBTNode.BTStatus.SUCCESS;
            }

            return AbstractBTNode.BTStatus.FAILURE;
        });

        BTNode pickUpTargetObject = new BTNode(() =>
        {
            IAction pickUpAction = TargetObject.GetAction(ActionID.PICK_UP);

            if (pickUpAction == null)
            {
                Debug.Log("Can't pick up: " + TargetObject.name);
                TargetObject = null;
                return AbstractBTNode.BTStatus.FAILURE;
            }
            TargetObject = null;

            agent.AddAction(pickUpAction);
            return AbstractBTNode.BTStatus.SUCCESS;
        });

        BTNode locateNearbyFood = new BTNode(() =>
        {
            //if(TargetObject == null)
            //{
                SmartObject food = BTFindSomething.SearchClosest<SmartObject>("Food", transform.position, 10);
                if (food != null)
                {
                    MoveTarget = food.transform.position;
                    TargetObject = food;
                    return AbstractBTNode.BTStatus.SUCCESS;
                }
                return AbstractBTNode.BTStatus.FAILURE;
            //}

            //return AbstractBTNode.BTStatus.SUCCESS;
        });

        BTNode locateNearbyTree = new BTNode(() =>
        {
            //if(TargetObject == null)
            //{
            SmartObject tree = BTFindSomething.SearchClosest<SmartObject>("Tree", transform.position, 10);
            if (tree != null)
            {
                MoveTarget = tree.transform.position;
                TargetObject = tree;
                return AbstractBTNode.BTStatus.SUCCESS;
            }
            return AbstractBTNode.BTStatus.FAILURE;
            //}

            //return AbstractBTNode.BTStatus.SUCCESS;
        });

        BTNode eatFoodInHand = new BTNode(() =>
        {
            if (carry.CarriedItem != null && carry.CarriedItem.TryGetAction(ActionID.EAT, out IEntityAction action))
            {
                agent.AddAction(action);
                carry.CarriedItem = null;
                return AbstractBTNode.BTStatus.SUCCESS;
            }
            return AbstractBTNode.BTStatus.FAILURE;
        });

        BTNode putIntoStorage = new BTNode(() =>
        {
            agent.AddAction(FoodContainer.GetAction(ActionID.PUT_IN));
            return AbstractBTNode.BTStatus.SUCCESS;
        });

        BTNode hasFood = new BTNode(() =>
        {
            return (carry.CarriedItem != null) ? AbstractBTNode.BTStatus.SUCCESS : AbstractBTNode.BTStatus.FAILURE;
        });

        BTNode attack = new BTNode(() =>
        {
            agent.AddAction(agent.GetAction(ActionID.ATTACK));
            return AbstractBTNode.BTStatus.SUCCESS;
        });

        BTSequence pickUpNearbyFoodSequence = new BTSequence(locateNearbyFood, moveTo, pickUpTargetObject);
        BTDecorator successToRunningDec1 = new BTDecorator(pickUpNearbyFoodSequence, SuccessToRunning);

        #region Hungry, eat food
        BTSequence takeFoodFromStorage = new BTSequence(selectFoodContainer, moveTo, takeOutFromStorage);
        BTDecorator successToRunningDec2 = new BTDecorator(takeFoodFromStorage, SuccessToRunning);

        BTSelector eatSelector = new BTSelector(eatFoodInHand, successToRunningDec1, successToRunningDec2);
        IPlan eatFoodPlan = new BTRoot(eatSelector, this);
        Decision eatDecision = new Decision(eatFoodPlan, (_) => 1 - (hunger.Food / 100)); //Linear utility, depending on how much food left
        #endregion

        #region GatherFood
        BTSequence putFoodIntoStorage = new BTSequence(hasFood, selectFoodContainer, moveTo, putIntoStorage);

        BTSelector gatherFoodSelector = new BTSelector(putFoodIntoStorage, successToRunningDec1);
        IPlan gatherFoodPlan = new BTRoot(gatherFoodSelector, this);
        Decision gatherFoodDecision = new Decision(gatherFoodPlan, (lastPlanRun) => 0.6f + 0.2f * (int)lastPlanRun);
        #endregion

        #region Cut Tree
        BTSequence cutTreeSequence = new BTSequence(locateNearbyTree, moveTo, attack);
        IPlan cutTreePlan = new BTRoot(cutTreeSequence, this);
        Decision cutTreeDecision = new Decision(cutTreePlan, (_) => 0.5f);
        #endregion

        State idleState = new State();
        idleState.AddTransaction(hungry);

        idleState.AddDecision(eatDecision);
        idleState.AddDecision(cutTreeDecision);
        idleState.AddDecision(gatherFoodDecision);

        StateMachine stateMachine = new StateMachine(this);

        sensor = stateMachine;

        stateMachine.SetState(idleState);
    }

    private IDecision CreateEatDecision()
    {
        return null;
    }
}
