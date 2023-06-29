using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationComponent : EntityComponent
{
    public override int ID => ComponentIDs.ANIMATION;

    [SerializeField]
    private AnimationEventListener animationController;
    [field: SerializeField]
    public Animator Animator { get; private set; }

    public void Play(AnimationClip animation, System.Action<int> OnEventFired)
    {
        animationController.SetListener(OnEventFired);
        Animator.Play(animation.name);
    }
}