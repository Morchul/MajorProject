using UnityEngine;

public class HumanAI : CharacterBaseAI
{
    private Human agent;

    [Header("Components")]
    [SerializeField]
    private HungerComponent hunger;
    [SerializeField]
    private CarryComponent carry;
    [SerializeField]
    private HumanPropertyComponent properties;

    protected override void Start()
    {
        agent = GetComponent<Human>();

        DecisionTransaction hungry = new DecisionTransaction(this, () => hunger.Food < 40, () => hunger.Food > 40);

        BTMoveTo moveTo = new BTMoveTo(agent, this);
        moveTo.MaxDistanceSqrt = 4;
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

        BTFindTarget findConstructionSite = new BTFindTarget(this, "ConstructionSite", agent.transform, 15);
        BTSetTarget setConstructionSite = new BTSetTarget(findConstructionSite);
        BTFindTarget findWood = new BTFindTarget(this, "Wood", agent.transform, 15);
        BTSetTarget setWood = new BTSetTarget(findWood);
        BTFindTarget findTree = new BTFindTarget(this, "Tree", agent.transform, 15);
        BTFindTarget findFood = new BTFindTarget(this, "Food", agent.transform, 15);
        BTFindTarget findFoodContainer = new BTFindTarget(this, "FoodContainer", agent.transform, 15);

        BTExecuteAction putInAction = new BTExecuteAction(this, agent, ActionID.PUT_IN);
        BTExecuteAction buildAction = new BTExecuteAction(this, agent, ActionID.BUILD);
        BTExecuteAction pickUpAction = new BTExecuteAction(this, agent, ActionID.PICK_UP);
        BTExecuteAction eatAction = new BTExecuteAction(this, agent, ActionID.EAT);
        BTExecuteAction takeOutAction = new BTExecuteAction(this, agent, ActionID.TAKE_OUT);

        BTExecuteAgentAction attackAction = new BTExecuteAgentAction(agent, ActionID.ATTACK);
        BTExecuteAgentAction dropAction = new BTExecuteAgentAction(agent, ActionID.DROP);

        BTPickRandomSpot pickRandomSpot = new BTPickRandomSpot(this);
        BTSuccessToRunning neverSuccedingMoveTo = new BTSuccessToRunning(moveTo);
        BTSequence randomWalk = new BTSequence("Random walk", pickRandomSpot, neverSuccedingMoveTo);

        BTSequence pickUpNearbyFoodSequence = new BTSequence("PickUpNearbyFood", findFood, moveTo, pickUpAction);
        BTSuccessToRunning continuesPickUpNearbyFoodSequence = new BTSuccessToRunning(pickUpNearbyFoodSequence);

        BTSuccessToFailure dropItemBecauseItIsWrong = new BTSuccessToFailure(dropAction);
        BTSuccessToFailure ignoreSetCarriedObjectAsTargetForSelector = new BTSuccessToFailure(setCarriedObjectAsTarget);

        BTSelector checkIfCarriedObjectIsEdible = new BTSelector("checkIfCarriedObjectIsEdible", ifTargetObjectIsEdible, dropItemBecauseItIsWrong);
        BTSequence setAndCheckCarriedItemIsEdible = new BTSequence("setAndCheckCarriedItem", setCarriedObjectAsTarget, checkIfCarriedObjectIsEdible);

        #region Hungry, eat food
        BTSequence takeFoodFromStorage = new BTSequence("take food from storage", findFoodContainer, moveTo, takeOutAction);
        BTSuccessToRunning continuesTakeFoodFromStorage = new BTSuccessToRunning(takeFoodFromStorage);

        BTSequence eatFoodInHandSequence = new BTSequence("eat food in hand", setAndCheckCarriedItemIsEdible, eatAction);

        BTSelector eatSelector = new BTSelector("eat", eatFoodInHandSequence, continuesPickUpNearbyFoodSequence, continuesTakeFoodFromStorage);
        IPlan eatFoodPlan = new BTRoot(eatSelector, this);
        Decision eatDecision = new Decision(eatFoodPlan, (_) => 1 - (hunger.Food / 100)); //Linear utility, depending on how much food left
        eatDecision.SuccessDecisionModifier.Set(0.15f, 0.5f);
        #endregion

        #region GatherFood
        BTSequence putFoodIntoStorage = new BTSequence("put food in storage", setAndCheckCarriedItemIsEdible, findFoodContainer, moveTo, putInAction);

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

        BTSequence buildTargetSequence = new BTSequence("build target", enoughWood, moveTo, buildAction);
        BTSequence bringWoodToConstructionSite = new BTSequence("bring wood to construction site", setAndCheckCarriedItemIsWood, findConstructionSite, moveTo, putInAction);
        BTSequence gatherWoodSequence = new BTSequence("gather wood", findWood, moveTo, pickUpAction);
        BTSequence cutTreeSequence = new BTSequence("cut tree sequence", findTree, moveTo, attackAction);
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

        State idleState = new State();
        idleState.AddTransaction(hungry);

        idleState.AddDecision(eatDecision);
        idleState.AddDecision(gatherFoodDecision);
        idleState.AddDecision(buildHouseDecision);

        State combatState = new State();

        StateMachine stateMachine = new StateMachine(this);

        sensor = stateMachine;

        stateMachine.SetState(idleState);
    }
}
