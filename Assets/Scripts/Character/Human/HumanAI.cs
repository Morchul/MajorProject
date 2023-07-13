using UnityEngine;

public class HumanAI : CharacterBaseAI
{
    [Header("Components")]
    [SerializeField]
    private HungerComponent hunger;
    [SerializeField]
    private CarryComponent carry;
    [SerializeField]
    private HumanPropertyComponent properties;

    [SerializeField]
    private Transform moveToDebugObject;

    protected override void Start()
    {
        base.Start();

        moveTo.debugObject = moveToDebugObject;

        State idleState = new State("Human Idle");
        State combatState = new State("Human combat");

        IContinuesTransaction hungry = new DecisionTransaction(this, () => hunger.Food < 40, () => hunger.Food > 40);
        ITriggerTransaction enterCombat = new TriggerTransaction(0, combatState, stateMachine, () => BTUtility.SearchClosest<Entity>("Blob", agent.transform.position, 15) != null);
        ITriggerTransaction exitCombat = new TriggerTransaction(0, idleState, stateMachine, () => BTUtility.SearchClosest<Entity>("Blob", agent.transform.position, 15) == null);

        BTExecuteAgentAction attackAction = new BTExecuteAgentAction(agent, ActionID.ATTACK);

        BTCheckTargetObject ifTargetObjectIsEdible = new BTCheckTargetObject(this, ComponentIDs.EDIBLE);
        BTCheckTargetObjectType ifTargetObjectIsTypeWood = new BTCheckTargetObjectType(this, ObjectType.WOOD);

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

        BTFindTargetObject findConstructionSite = new BTFindTargetObject(this, "ConstructionSite", agent.transform, 15);
        BTSetTargetObject setConstructionSite = new BTSetTargetObject(findConstructionSite);
        BTFindTargetObject findWood = new BTFindTargetObject(this, "Wood", agent.transform, 15);
        BTSetTargetObject setWood = new BTSetTargetObject(findWood);
        BTFindTargetObject findTree = new BTFindTargetObject(this, "Tree", agent.transform, 15);
        BTFindTargetObject findFood = new BTFindTargetObject(this, "Food", agent.transform, 15);
        BTFindTargetObject findFoodContainer = new BTFindTargetObject(this, "FoodContainer", agent.transform, 15);

        BTPickRandomSpot pickRandomSpot = new BTPickRandomSpot(this);
        BTSuccessToRunning neverSuccedingMoveTo = new BTSuccessToRunning(moveTo);
        BTSequence randomWalk = new BTSequence("Random walk", pickRandomSpot, neverSuccedingMoveTo);

        BTSequence pickUpNearbyFoodSequence = new BTSequence("PickUpNearbyFood", findFood, moveToTargetObject, pickUpAction);
        BTSuccessToRunning continuesPickUpNearbyFoodSequence = new BTSuccessToRunning(pickUpNearbyFoodSequence);

        BTSuccessToFailure dropItemBecauseItIsWrong = new BTSuccessToFailure(dropAction);
        //BTSuccessToFailure ignoreSetCarriedObjectAsTargetForSelector = new BTSuccessToFailure(setCarriedObjectAsTarget);

        BTSelector checkIfCarriedObjectIsEdible = new BTSelector("checkIfCarriedObjectIsEdible", ifTargetObjectIsEdible, dropItemBecauseItIsWrong);
        BTSequence setAndCheckCarriedItemIsEdible = new BTSequence("setAndCheckCarriedItem", setCarriedObjectAsTarget, checkIfCarriedObjectIsEdible);

        #region Hungry, eat food
        BTSequence takeFoodFromStorage = new BTSequence("take food from storage", findFoodContainer, moveToTargetObject, takeOutAction);
        BTSuccessToRunning continuesTakeFoodFromStorage = new BTSuccessToRunning(takeFoodFromStorage);

        BTSequence eatFoodInHandSequence = new BTSequence("eat food in hand", setAndCheckCarriedItemIsEdible, eatAction);

        BTSelector eatSelector = new BTSelector("eat", eatFoodInHandSequence, continuesPickUpNearbyFoodSequence, continuesTakeFoodFromStorage);
        IPlan eatFoodPlan = new BTRoot(eatSelector, this);
        Decision eatDecision = new Decision(eatFoodPlan, (_) => 1 - (hunger.Food / 100)); //Linear utility, depending on how much food left
        eatDecision.SuccessDecisionModifier.Set(0.15f, 0.5f);
        #endregion

        #region GatherFood
        BTSequence putFoodIntoStorage = new BTSequence("put food in storage", setAndCheckCarriedItemIsEdible, findFoodContainer, moveToTargetObject, putInAction);

        BTSelector gatherFoodSelector = new BTSelector("gather food", putFoodIntoStorage, continuesPickUpNearbyFoodSequence);
        IPlan gatherFoodPlan = new BTRoot(gatherFoodSelector, this);

        Decision gatherFoodDecision = new Decision(gatherFoodPlan, (_) => 0.6f);
        gatherFoodDecision.RunningDecisionModifier.Set(0.2f, 2f);
        gatherFoodDecision.FailedDecisionModifier.Set(-0.3f, 3f);
        #endregion

        #region Build House
        BTTest enoughWood = new BTTest("Enough wood test", () => findConstructionSite.TargetObject.GetComponent<BuildComponent>(ComponentIDs.BUILD).AbleToBuild());

        BTSelector checkIfCarriedObjectIsWood = new BTSelector("checkIfCarriedObjectIsWood", ifTargetObjectIsTypeWood, dropItemBecauseItIsWrong);
        BTSequence setAndCheckCarriedItemIsWood = new BTSequence("setAndCheckCarriedItem", setCarriedObjectAsTarget, checkIfCarriedObjectIsWood);

        BTSequence buildTargetSequence = new BTSequence("build target", enoughWood, moveToTargetObject, buildAction);
        BTSequence bringWoodToConstructionSite = new BTSequence("bring wood to construction site", setAndCheckCarriedItemIsWood, findConstructionSite, moveToTargetObject, putInAction);
        BTSequence gatherWoodSequence = new BTSequence("gather wood", findWood, moveToTargetObject, pickUpAction);
        BTSequence cutTreeSequence = new BTSequence("cut tree sequence", findTree, moveToTargetObject, attackAction);
        BTSelector cutTreeSelector = new BTSelector("cut tree selector", cutTreeSequence);//, randomWalk); //Random walk does not work

        //BTSuccessToRunning continuesBuildTargetSequence = new BTSuccessToRunning(buildTargetSequence);
        //BTSuccessToRunning continuesGatherWoodSequence = new BTSuccessToRunning(gatherWoodSequence);
        //BTSuccessToRunning continuesCutTreeSelector = new BTSuccessToRunning(cutTreeSelector);

        BTSelector buildHouseGatherWood = new BTSelector("build hosue gather resources", bringWoodToConstructionSite, gatherWoodSequence, cutTreeSequence);
        BTSuccessToRunning continuesGatherWood = new BTSuccessToRunning(buildHouseGatherWood);

        BTSelector buildHouseSelector = new BTSelector("build house selector", buildTargetSequence, continuesGatherWood);
        BTSequence buildHouseSequence = new BTSequence("build house sequence", findConstructionSite, buildHouseSelector);
        IPlan buildHousePlan = new BTRoot(buildHouseSequence, this);
        Decision buildHouseDecision = new Decision(buildHousePlan, (_) => 0.4f);
        #endregion

        #region Attack Blob

        BTFindTargetEntity findBlob = new BTFindTargetEntity(this, "Blob", agent.transform, 15);
        BTTriggerTransaction exitCombatTrigger = new BTTriggerTransaction(exitCombat);

        BTSequence killBlobSequence = new BTSequence("Kill blob", findBlob, moveToTargetEntity, attackAction);
        BTSelector killBlobSelector = new BTSelector("Kill blob selector", killBlobSequence, exitCombatTrigger);
        IPlan killBlobPlan = new BTRoot(killBlobSelector, this);
        IDecision killBlob = new Decision(killBlobPlan, (_) => 1f);
        #endregion

        idleState.AddTransaction(hungry);

        idleState.AddDecision(eatDecision);
        idleState.AddDecision(gatherFoodDecision);
        idleState.AddDecision(buildHouseDecision);

        combatState.AddDecision(killBlob);

        idleState.AddTransaction(enterCombat);
        combatState.AddTransaction(exitCombat);

        stateMachine.SetState(idleState);
    }
}
