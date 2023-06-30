using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobAI : CharacterBaseAI
{
    [Header("Components")]
    [SerializeField]
    private CarryComponent carry;

    protected override void Start()
    {
        base.Start();

        BTMoveTo moveTo = new BTMoveTo(agent, this);
        moveTo.MaxDistanceSqrt = 4;

        BTFindTargetObject findFood = new BTFindTargetObject(this, "Food", agent.transform, 10);
        BTFindTargetObject findFoodContainer = new BTFindTargetObject(this, "FoodContainer", agent.transform, 10);

        BTSequence pickUpNearbyFoodSequence = new BTSequence("PickUpNearbyFood", findFood, moveTo, pickUpAction);
        BTSuccessToRunning continuesPickUpNearbyFoodSequence = new BTSuccessToRunning(pickUpNearbyFoodSequence);

        BTCheckTargetObject ifTargetObjectIsEdible = new BTCheckTargetObject(this, ComponentIDs.EDIBLE);

        BTNode setCarriedObjectAsTarget = new BTNode("Set carried- as targetobject", () =>
        {
            if (carry.CarriedItem != null)
            {
                TargetObject = carry.CarriedItem;
                return AbstractBTNode.BTStatus.SUCCESS;
            }
            else
                return AbstractBTNode.BTStatus.FAILURE;

        });

        BTSuccessToFailure dropItemBecauseItIsWrong = new BTSuccessToFailure(dropAction);

        BTSelector checkIfCarriedObjectIsEdible = new BTSelector("checkIfCarriedObjectIsEdible", ifTargetObjectIsEdible, dropItemBecauseItIsWrong);
        BTSequence setAndCheckCarriedItemIsEdible = new BTSequence("setAndCheckCarriedItem", setCarriedObjectAsTarget, checkIfCarriedObjectIsEdible);

        BTSequence takeFoodFromStorage = new BTSequence("take food from storage", findFoodContainer, moveTo, takeOutAction);
        BTSuccessToRunning continuesTakeFoodFromStorage = new BTSuccessToRunning(takeFoodFromStorage);

        BTSequence eatFoodInHandSequence = new BTSequence("eat food in hand", setAndCheckCarriedItemIsEdible, eatAction);

        BTSelector eatSelector = new BTSelector("eat", eatFoodInHandSequence, continuesPickUpNearbyFoodSequence, continuesTakeFoodFromStorage);

        IPlan eatFoodPlan = new BTRoot(eatSelector, this);
        Decision eatFood = new Decision(eatFoodPlan, (_) => 1f);

        State idleState = new State("Blob Idle");
        idleState.AddDecision(eatFood);

        StateMachine stateMachine = new StateMachine(this);

        Sensor = stateMachine;

        stateMachine.SetState(idleState);
    }
}
