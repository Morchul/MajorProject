using NUnit.Framework;

public class ActionRingBufferTest
{
    [Test]
    public void ActionRingBufferRemoveTest()
    {
        TestAction action1 = new TestAction("a1");
        TestAction action2 = new TestAction("a2");
        TestAction action3 = new TestAction("a3");
        TestAction action4 = new TestAction("a4");

        ActionRingBuffer ringBuffer = new ActionRingBuffer(3);

        action1.Status = ActionState.ACTIVE;
        action2.Status = ActionState.ACTIVE;
        action3.Status = ActionState.ACTIVE;
        action4.Status = ActionState.ACTIVE;

        ringBuffer.Add(action1);
        ringBuffer.Add(action2);

        //----------Current Ring Buffer-----
        // Action1 / Action2 / null / null
        //  Head               Tail
        //----------------------------------

        action2.Status = ActionState.INACTIVE;
        ringBuffer.Remove();

        ringBuffer.Add(action3);

        //----------Current Ring Buffer-----
        // Action1 / Action2 / Action3 / null
        //  Head                         Tail
        //----------------------------------

        Assert.AreEqual(ringBuffer[0].Name, "a1");
        Assert.AreEqual(ringBuffer[1].Name, "a2");
        Assert.AreEqual(ringBuffer[2].Name, "a3");

        Assert.AreEqual(ringBuffer.Length, 3);
        Assert.AreEqual(ringBuffer.Head, 0);
        Assert.AreEqual(ringBuffer.Tail, 3);

        action1.Status = ActionState.INACTIVE;
        ringBuffer.Remove();

        //----------Current Ring Buffer-----
        // Action1 / Action2 / Action3 / null
        //                      Head     Tail
        //----------------------------------

        Assert.AreEqual(ringBuffer[0].Name, "a3");

        Assert.AreEqual(ringBuffer.Length, 1);
        Assert.AreEqual(ringBuffer.Head, 2);
        Assert.AreEqual(ringBuffer.Tail, 3);

        ringBuffer.Add(action1);
        ringBuffer.Add(action4);

        //----------Current Ring Buffer-----
        // Action4 / Action2 / Action3 / Action1
        //             Tail     Head     
        //----------------------------------

        Assert.AreEqual(ringBuffer[0].Name, "a3");
        Assert.AreEqual(ringBuffer[1].Name, "a1");
        Assert.AreEqual(ringBuffer[2].Name, "a4");

        Assert.AreEqual(ringBuffer.Length, 3);
        Assert.AreEqual(ringBuffer.Head, 2);
        Assert.AreEqual(ringBuffer.Tail, 1);
    }

    [Test]
    public void ActionRingBufferAddTest()
    {
        TestAction action1 = new TestAction("a1");
        TestAction action2 = new TestAction("a2");
        TestAction action3 = new TestAction("a3");
        TestAction action4 = new TestAction("a4");

        ActionRingBuffer ringBuffer = new ActionRingBuffer(3);

        action1.Status = ActionState.ACTIVE;
        action2.Status = ActionState.ACTIVE;
        action3.Status = ActionState.ACTIVE;
        action4.Status = ActionState.ACTIVE;

        ringBuffer.Add(action1);
        ringBuffer.Add(action2);

        //----------Current Ring Buffer-----
        // Action1 / Action2 / null / null
        //  Head               Tail
        //----------------------------------

        action1.Status = ActionState.INACTIVE;
        action2.Status = ActionState.INACTIVE;

        Assert.AreEqual(ringBuffer.Length, 2);
        Assert.AreEqual(ringBuffer.Head, 0);
        Assert.AreEqual(ringBuffer.Tail, 2);

        ringBuffer.Remove();

        //----------Current Ring Buffer-----
        // Action1 / Action2 / null /null
        //                     Head
        //                     Tail
        //----------------------------------

        Assert.IsTrue(ringBuffer.Empty);

        ringBuffer.Add(action3);
        ringBuffer.Add(action4);

        //----------Current Ring Buffer-----
        // Action1 / Action2 / Action3 // Action4
        //   Tail               Head
        //----------------------------------

        Assert.AreEqual(ringBuffer[0].Name, "a3");
        Assert.AreEqual(ringBuffer[1].Name, "a4");
        Assert.Throws<System.Exception>(() => ringBuffer[2] = null);

        Assert.AreEqual(ringBuffer.Length, 2);
        Assert.AreEqual(ringBuffer.Head, 2);
        Assert.AreEqual(ringBuffer.Tail, 0);

        action1.Status = ActionState.ACTIVE;
        action2.Status = ActionState.ACTIVE;

        ringBuffer.Add(action2);

        Assert.AreEqual(ringBuffer.Head, 2);
        Assert.AreEqual(ringBuffer.Tail, 1);

        Assert.Throws<System.Exception>(() => ringBuffer.Add(action1));

        Assert.AreEqual(ringBuffer.Head, 2);
        Assert.AreEqual(ringBuffer.Tail, 1);

        //----------Current Ring Buffer-----
        // Action2 / Action2 / Action3 // Action4
        //             Tail      Head          
        //----------------------------------

        action4.Status = ActionState.INACTIVE;
        ringBuffer.Add(action1);

        //----------Current Ring Buffer-----
        // Action2 / Action1 / Action3 // Action4
        //   Head                          Tail
        //----------------------------------

        Assert.AreEqual(ringBuffer[0].Name, "a2");
        Assert.AreEqual(ringBuffer[1].Name, "a1");
        Assert.AreEqual(ringBuffer[2].Name, "a3");

        Assert.AreEqual(ringBuffer.Length, 3);
        Assert.AreEqual(ringBuffer.Head, 0);
        Assert.AreEqual(ringBuffer.Tail, 3);
    }

}
