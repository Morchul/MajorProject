using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanAI : AI
{
    private Human agent;

    public Food TargetFood;
    public FoodContainer FoodContainer;

    public Vector3 MoveTarget;

    private void Start()
    {
        agent = GetComponent<Human>();
        HungerComponent hunger = agent.GetComponent<HungerComponent>(ComponentIDs.HUNGER);
        CarryComponent carry = agent.GetComponent<CarryComponent>(ComponentIDs.CARRY);

        DecisionTransaction hungry = new DecisionTransaction(this, () => hunger.Food < 40, () => hunger.Food > 40);

        BTMoveTo moveTo = new BTMoveTo(agent, this);
        moveTo.MaxDistanceSqrt = 4;

        AbstractBTNode eatAction = new BTEat(agent, this);
        BTLocateFood locateNearbyFood = new BTLocateFood(this);
        BTGetFoodFromContainer getFoodFromContainer = new BTGetFoodFromContainer(this);

        BTNode selectFoodContainer = new BTNode(() =>
        {
            FoodContainer = BTFindSomething.SearchClosest<FoodContainer>("FoodContainer", transform.position, 10);
            MoveTarget = FoodContainer.transform.position;
            return (FoodContainer == null) ? AbstractBTNode.BTStatus.FAILURE : AbstractBTNode.BTStatus.SUCCESS;
        });

        BTNode takeFoodFromStorage = new BTNode(() =>
        {
            TargetFood = FoodContainer.TakeOut();
            if (TargetFood == null) return AbstractBTNode.BTStatus.FAILURE;
            return AbstractBTNode.BTStatus.SUCCESS;
        });

        BTSequence eatFoodFromContainer = new BTSequence(selectFoodContainer, moveTo, takeFoodFromStorage, eatAction);
        BTSequence eatNearbyFoodSequence = new BTSequence(locateNearbyFood, moveTo, eatAction);
        BTSelector locateFood = new BTSelector(eatNearbyFoodSequence, eatFoodFromContainer);
        IPlan eatPlan = new BTRoot(locateFood, this);

        BTNode pickUpFood = new BTNode(() =>
        {
            agent.AddAction(TargetFood.PickUpAction);
            return AbstractBTNode.BTStatus.SUCCESS;
        });

        BTSequence getFood = new BTSequence(locateNearbyFood, moveTo, pickUpFood);

        AbstractBTNode pickRandomCloseSpot = new BTPickRandomSpot(this);
        AbstractBTNode removeMoveTo = new BTNode(() => { MoveTarget = Vector3.zero; return AbstractBTNode.BTStatus.SUCCESS; });

        BTSequence moveAroundSequence = new BTSequence(pickRandomCloseSpot, moveTo, removeMoveTo);

        IPlan moveAroundPlan = new BTRoot(moveAroundSequence, this);

        Decision eatDecision = new Decision(eatPlan, () => 1 - (hunger.Food / 100)); //Linear utility, depending on how much food left
        Decision moveAround = new Decision(moveAroundPlan, () => 0.5f);


        #region GatherFood and put it in storage container

        BTNode putIntoStorage = new BTNode(() =>
        {
            agent.AddAction(FoodContainer.PutInAction);
            return AbstractBTNode.BTStatus.SUCCESS;
        });

        BTSequence deliverFood = new BTSequence(selectFoodContainer, moveTo, putIntoStorage);
        BTDecorator nothingInHandDecorator = new BTDecorator(getFood, (_) => carry.CarriedItem == null ? AbstractBTNode.BTStatus.SUCCESS : AbstractBTNode.BTStatus.FAILURE);
        BTSelector gatherFoodSelector = new BTSelector(nothingInHandDecorator, deliverFood);
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
