using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAction
{
    public void Update();
    public void Interrupt();
    public void Execute();

    public int Layer { get; }

    public ActionState Status { get; set; }
}
