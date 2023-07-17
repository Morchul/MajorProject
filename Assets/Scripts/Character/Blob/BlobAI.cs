using UnityEngine;

public class BlobAI : CharacterBaseAI
{
    [Header("Components")]
    [SerializeField]
    private CarryComponent carry;

    protected override void Start()
    {
        base.Start();

        BTFindTargetObject findFood = new BTFindTargetObject(this, "Food", agent.transform, 10);

        BTSequence eatSequence = new BTSequence("Blob eat sequence", findFood, moveToTargetObject, eatAction);

        IPlan eatFoodPlan = new BTRoot(eatSequence, this);
        Decision eatFood = new Decision(eatFoodPlan, (_) => 1f);

        State idleState = new State("Blob Idle");
        idleState.AddDecision(eatFood);

        stateMachine.SetState(idleState);


        //BTFindTargetObject findFoodContainer = new BTFindTargetObject(this, "FoodContainer", agent.transform, 10);

        //BTSequence pickUpNearbyFoodSequence = new BTSequence("PickUpNearbyFood", findFood, moveToTargetObject, pickUpAction);
        //BTSuccessToRunning continuesPickUpNearbyFoodSequence = new BTSuccessToRunning(pickUpNearbyFoodSequence);

        //BTCheckTargetObject ifTargetObjectIsEdible = new BTCheckTargetObject(this, ComponentIDs.EDIBLE);

        //BTNode setCarriedObjectAsTarget = new BTNode("Set carried- as targetobject", () =>
        //{
        //    if (carry.CarriedItem != null)
        //    {
        //        TargetObject = carry.CarriedItem;
        //        return AbstractBTNode.BTStatus.SUCCESS;
        //    }
        //    else
        //        return AbstractBTNode.BTStatus.FAILURE;

        //});

        //BTSuccessToFailure dropItemBecauseItIsWrong = new BTSuccessToFailure(dropAction);

        //BTSelector checkIfCarriedObjectIsEdible = new BTSelector("checkIfCarriedObjectIsEdible", ifTargetObjectIsEdible, dropItemBecauseItIsWrong);
        //BTSequence setAndCheckCarriedItemIsEdible = new BTSequence("setAndCheckCarriedItem", setCarriedObjectAsTarget, checkIfCarriedObjectIsEdible);

        //BTSequence takeFoodFromStorage = new BTSequence("take food from storage", findFoodContainer, moveToTargetObject, takeOutAction);
        //BTSuccessToRunning continuesTakeFoodFromStorage = new BTSuccessToRunning(takeFoodFromStorage);

        //BTSequence eatFoodInHandSequence = new BTSequence("eat food in hand", setAndCheckCarriedItemIsEdible, eatAction);

        //BTSelector eatSelector = new BTSelector("eat", eatFoodInHandSequence, continuesPickUpNearbyFoodSequence, continuesTakeFoodFromStorage);
    }
}
