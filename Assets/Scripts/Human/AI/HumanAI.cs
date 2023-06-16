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
    public ISmartObject TargetObject;
    //public Food TargetFood;
    public FoodContainer FoodContainer;

    public Vector3 MoveTarget;

    private void Start()
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
            if (FoodContainer.TryGetAction(ActionID.TAKE_OUT, out IAction action))
            {
                agent.AddAction(action);
                return AbstractBTNode.BTStatus.SUCCESS;
            }
            return AbstractBTNode.BTStatus.FAILURE;
        });

        BTNode selectFoodContainer = new BTNode(() =>
        {
            FoodContainer = BTFindSomething.SearchClosest<FoodContainer>("FoodContainer", transform.position, 10);
            MoveTarget = FoodContainer.transform.position;
            return (FoodContainer == null) ? AbstractBTNode.BTStatus.FAILURE : AbstractBTNode.BTStatus.SUCCESS;
        });

        BTNode pickUpFood = new BTNode(() =>
        {
            agent.AddAction(TargetObject.GetAction(ActionID.PICK_UP));
            TargetObject = null;
            return AbstractBTNode.BTStatus.SUCCESS;
        });

        BTNode locateNearbyFood = new BTNode(() =>
        {
            SmartObject food = BTFindSomething.SearchClosest<SmartObject>("Food", transform.position, 10);
            if(food != null)
            {
                MoveTarget = food.transform.position;
                TargetObject = food;
                return AbstractBTNode.BTStatus.SUCCESS;
            }

            return AbstractBTNode.BTStatus.FAILURE;
        });
        BTNode eatFoodInHand = new BTNode(() =>
        {
            if (carry.CarriedItem != null && carry.CarriedItem.TryGetAction(ActionID.EAT, out IAction action))
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

        BTSequence pickUpNearbyFoodSequence = new BTSequence(locateNearbyFood, moveTo, pickUpFood);
        BTDecorator successToRunningDec1 = new BTDecorator(pickUpNearbyFoodSequence, SuccessToRunning);

        #region Hungry, eat food
        BTSequence takeFoodFromStorage = new BTSequence(selectFoodContainer, moveTo, takeOutFromStorage);
        BTDecorator successToRunningDec2 = new BTDecorator(takeFoodFromStorage, SuccessToRunning);

        BTSelector eatSelector = new BTSelector(eatFoodInHand, successToRunningDec1, successToRunningDec2);
        IPlan eatFoodPlan = new BTRoot(eatSelector, this);
        Decision eatDecision = new Decision(eatFoodPlan, () => 1 - (hunger.Food / 100)); //Linear utility, depending on how much food left
        #endregion

        #region GatherFood
        BTSequence putFoodIntoStorage = new BTSequence(hasFood, selectFoodContainer, moveTo, putIntoStorage);

        BTSelector gatherFoodSelector = new BTSelector(putFoodIntoStorage, successToRunningDec1);
        IPlan gatherFoodPlan = new BTRoot(gatherFoodSelector, this);
        Decision gatherFoodDecision = new Decision(gatherFoodPlan, () => 0.6f);
        #endregion



        State idleState = new State();
        idleState.AddTransaction(hungry);

        idleState.AddDecision(eatDecision);
        //idleState.AddDecision(moveAround);
        idleState.AddDecision(gatherFoodDecision);

        StateMachine stateMachine = new StateMachine(this);

        sensor = stateMachine;

        stateMachine.SetState(idleState);
    }
}
