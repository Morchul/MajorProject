using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAction : IAction<BaseEntity>
{
    public int Layer => throw new System.NotImplementedException();

    public ActionState Status { get; set; }

    public string Name { get; set; }

    public TestAction(string name = "")
    {
        Name = name;
    }

    public void Execute(BaseEntity executioner)
    {
        throw new System.NotImplementedException();
    }

    public void Interrupt()
    {
        throw new System.NotImplementedException();
    }

    public void Update()
    {
        throw new System.NotImplementedException();
    }
}
