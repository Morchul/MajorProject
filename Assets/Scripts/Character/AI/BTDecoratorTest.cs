public class BTDecoratorTest : AbstractBTDecorator
{
    private readonly System.Func<bool> TestFunc;

    public BTDecoratorTest(System.Func<bool> testFunc, AbstractBTNode child) : base(child)
    {
        TestFunc = testFunc;
    }

    public override BTStatus Tick()
    {
        if (TestFunc())
            return child.Tick();
        return BTStatus.FAILURE;
    }
}


public class BTTest : AbstractBTNode
{
    private readonly System.Func<bool> TestFunc;

    public BTTest(string name, System.Func<bool> testFunc)
    {
        TestFunc = testFunc;
        Name = name;
    }

    public override BTStatus Tick() => TestFunc() ? BTStatus.SUCCESS : BTStatus.FAILURE;
    public override void CleanUp() { }
}
