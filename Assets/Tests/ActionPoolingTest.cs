using NUnit.Framework;

public class ActionPoolingTest
{

    private int testCounter;

    [Test]
    public void ActionPoolingFactoryTest()
    {
        TestActionFactory factory = new TestActionFactory(CreateMethod);

        IEntityAction testAction1 = factory.GetAction(false);
        IEntityAction testAction2 = factory.GetAction(false);

        Assert.AreEqual(testCounter, 2);

        factory.ReturnAction(testAction2);
        IEntityAction testAction3 = factory.GetAction(false);

        Assert.AreEqual(testCounter, 2);
    }

    public void ActionPoolingAutoReturnTest()
    {
        TestActionFactory factory = new TestActionFactory(CreateMethod);

        IEntityAction testAction1 = factory.GetAction(true);
        IEntityAction testAction2 = factory.GetAction(false);

        Assert.AreEqual(testCounter, 2);

        testAction1.Status = ActionState.INACTIVE;
        IEntityAction testAction3 = factory.GetAction(false);

        Assert.AreEqual(testCounter, 2);
    }

    public IEntityAction CreateMethod()
    {
        return new TestAction("Test" + ++testCounter);
    }
}


public class TestActionFactory : ActionFactory
{
    public TestActionFactory(System.Func<IEntityAction> createMethod) : base(createMethod, 0)
    {

    }
}
