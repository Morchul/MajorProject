using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationComponent : EntityComponent
{
    public override int ID => ComponentIDs.ANIMATION;

    public override ActionComponent[] GetComponentActions() => null;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Animation(Animation animation)
    {
        //animator.Play()
    }
}
