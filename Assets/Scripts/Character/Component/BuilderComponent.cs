using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuilderComponent : EntityComponent
{
    public AnimationClip BuildAnimation;

    public abstract int Progress { get; }

    public override int ID => ComponentIDs.BUILDER;
}
