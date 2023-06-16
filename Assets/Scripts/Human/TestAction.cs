using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAction : IAction
{
    public int Layer => throw new System.NotImplementedException();
    public int ActionUsage => throw new System.NotImplementedException();

    public ActionState Status { get; set; }
    public string Name { get; set; }
    public ActionID ID => 0;

    public TestAction(string name = "")
    {
        Name = name;
    }

    public event Action OnInactive;

    public void Execute(Entity executioner)
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

    public void Init(Entity entity)
    {
        throw new NotImplementedException();
    }

    public void Return()
    {
        throw new NotImplementedException();
    }
}
