using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAction : BaseAction
{
    public override int Layer => 0;

    private string name;
    public override string Name => name;

    public override ActionID ID => 0;

    public TestAction(string name)
    {
        this.name = name;
    }

    protected override int[] GetMandatoryComponentIDs() => new int[] { };
}
