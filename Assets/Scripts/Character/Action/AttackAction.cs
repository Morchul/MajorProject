using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : BaseAction
{
    public override int Layer => (int)(ActionLayers.ARMS | ActionLayers.LEGS);

    public override string Name => "Attack";

    public override ActionID ID => ActionID.ATTACK;

    private CarryComponent carry;
    private AttackComponent attackComponent;
    private AnimationComponent animationComponent;

    public override void Init(Entity entity)
    {
        carry = entity.GetComponent<CarryComponent>(ComponentIDs.CARRY);
        attackComponent = entity.GetComponent<AttackComponent>(ComponentIDs.ATTACK);
        animationComponent = entity.GetComponent<AnimationComponent>(ComponentIDs.ANIMATION);
    }

    public override void Execute(Entity entity)
    {
        void AttackEvent(int eventID, AttackComponent attackComp)
        {
            if (eventID == 0)
                ActionFinished();
            else if (eventID == 1)
                DealDamage(attackComp, entity.transform.position);
        };

        if (carry != null && 
            carry.CarriedItem != null &&
            carry.CarriedItem.TryGetComponent(ComponentIDs.ATTACK, out AttackComponent attackComp))
        {
            animationComponent.Play(attackComp.attackAnim, (id) => AttackEvent(id, attackComp));
        }
        else
        {
            animationComponent.Play(attackComponent.attackAnim, (id) => AttackEvent(id, attackComponent));
        }
    }


    protected override int[] GetMandatoryComponentIDs() => new int[] {  };

    private void DealDamage(AttackComponent attackComponent, Vector3 pos)
    {
        Collider[] colliders = Physics.OverlapBox(pos, attackComponent.HitArea / 2, Quaternion.identity);
        foreach (Collider collider in colliders)
        {
            HealthComponent health = collider.GetComponent<HealthComponent>();
            if (health != null)
            {
                health.TakeDamage(attackComponent.Damage);
            }
        }
    }
}
