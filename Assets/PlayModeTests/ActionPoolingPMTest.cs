using NUnit.Framework;
using System.Collections;
using Unity.PerformanceTesting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class ActionPoolingTests : MonoBehaviour
{
    [OneTimeSetUp]
    public void LoadScene()
    {
        SceneManager.LoadScene("TestCase1");
    }

    [UnityTest]
    public IEnumerator UnityActionContainerPoolingTest()
    {
        yield return new WaitForEndOfFrame();

        Human human = GameObject.Find("Paladin").GetComponent<Human>();

        ActionComponent testActionComp = new ActionComponent()
        {
            ActionID = ActionID.ATTACK,
            StartContainerSize = 2,
            MaxContainerSize = 4,
        };

        ActionContainer testContainer = new ActionContainer(human, testActionComp);

        //2 inactive in container, 0 in factory
        Assert.AreEqual(GlobalActionFactory.GetFactory(ActionID.ATTACK).PoolSize, 0);
        Assert.AreEqual(testContainer.PoolSize, 2);

        IEntityAction action1 = testContainer.GetAction();
        action1.Status = ActionState.ACTIVE;
        IEntityAction action2 = testContainer.GetAction();
        action2.Status = ActionState.ACTIVE;
        IEntityAction action3 = testContainer.GetAction();
        action3.Status = ActionState.ACTIVE;

        //3 active in container, 0 in factory
        Assert.AreEqual(GlobalActionFactory.GetFactory(ActionID.ATTACK).PoolSize, 0);
        Assert.AreEqual(testContainer.PoolSize, 3);

        action1.Status = ActionState.INACTIVE;

        //2 active and 1 inactive in container
        Assert.AreEqual(testContainer.PoolSize, 3);

        IEntityAction action4 = testContainer.GetAction();
        action4.Status = ActionState.ACTIVE;

        //3 active in container
        Assert.AreEqual(testContainer.PoolSize, 3);

        IEntityAction action5 = testContainer.GetAction();
        action5.Status = ActionState.ACTIVE;

        //action 6 now exceeds max size and will be handled by factory
        IEntityAction action6 = testContainer.GetAction();
        action6.Status = ActionState.ACTIVE;

        //4 active in container
        Assert.AreEqual(testContainer.PoolSize, 4);

        action5.Status = ActionState.INACTIVE;

        IEntityAction action7 = testContainer.GetAction();
        action7.Status = ActionState.ACTIVE;

        //4 active in container
        Assert.AreEqual(testContainer.PoolSize, 4);

        action6.Status = ActionState.INACTIVE;

        //4 active in container, 1 in factory
        Assert.AreEqual(GlobalActionFactory.GetFactory(ActionID.ATTACK).PoolSize, 1);
        Assert.AreEqual(testContainer.PoolSize, 4);

        IEntityAction action8 = testContainer.GetAction();

        //4 active in container, 0 in factory
        Assert.AreEqual(GlobalActionFactory.GetFactory(ActionID.ATTACK).PoolSize, 0);
        Assert.AreEqual(testContainer.PoolSize, 4);

        yield return null;
    }
}
