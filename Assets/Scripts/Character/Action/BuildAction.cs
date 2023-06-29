using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildAction : BaseAction
{
    public override int Layer => (int)ActionLayers.ALL;

    public override string Name => "Build";

    public override ActionID ID => ActionID.BUILD;

    protected override int[] GetMandatoryComponentIDs() => new int[] { ComponentIDs.BUILDER, ComponentIDs.ANIMATION };


    private BuildComponent buildComponent;

    public override void Init(Entity entity)
    {
        buildComponent = entity.GetComponent<BuildComponent>(ComponentIDs.BUILD);
    }

    public override void Execute(Entity entity)
    {
        if (buildComponent.AbleToBuild())
        {
            AnimationComponent animationComponent = entity.GetComponent<AnimationComponent>(ComponentIDs.ANIMATION);
            BuilderComponent builderComponent = entity.GetComponent<BuilderComponent>(ComponentIDs.BUILDER);

            void AnimationFinished()
            {
                buildComponent.Build(builderComponent.Progress);
                ActionFinished();
            }

            animationComponent.Play(builderComponent.BuildAnimation, (_) => AnimationFinished());
        }
    }
}
