using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : BaseAction
{
    public override int Layer => 0;
    public override string Name => "Move";
    public override ActionID ID => ActionID.MOVE;
    protected override int[] GetMandatoryComponentIDs() => new int[] { ComponentIDs.MOVE };

    public Vector3 MoveDir { get; set; }

    private MoveComponent moveComponent;
    private AnimationComponent animationComponent;

    public override void Init(Entity entity)
    {
        moveComponent = entity.GetComponent<MoveComponent>(ComponentIDs.MOVE);
        animationComponent = entity.GetComponent<AnimationComponent>(ComponentIDs.ANIMATION);
    }

    public override void Execute(Entity entity)
    {
        //animationComponent.Animator.SetFloat("SpeedForward", 1);
    }

    public override void Update()
    {
        if (Status == ActionState.INTERRUPTED)
        {
            animationComponent.Animator.SetFloat("SpeedForward", 0);
            ActionFinished();
            return;
        }

        //moveComponent.MoveForward();
        animationComponent.Animator.SetFloat("SpeedForward", MoveDir.z);
        moveComponent.Move(MoveDir);
    }
}
