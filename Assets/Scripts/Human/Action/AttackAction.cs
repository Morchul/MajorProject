using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : BaseAction
{
    public override int Layer => (int)(ActionLayers.ARMS);

    public override string Name => "Attack";

    public override ActionID ID => ActionID.ATTACK;

    private CarryComponent carry;
    private AttackComponent attackComponent;

    public override void Init(Entity entity)
    {
        carry = entity.GetComponent<CarryComponent>(ComponentIDs.CARRY);
        attackComponent = entity.GetComponent<AttackComponent>(ComponentIDs.ATTACK);
    }

    public override void Execute(Entity entity)
    {
        if(carry != null && 
            carry.CarriedItem != null &&
            carry.CarriedItem.TryGetComponent(ComponentIDs.ATTACK, out AttackComponent attackComp))
        {
            DealDamage(attackComp, entity.transform.position);
        }
        else
        {
            DealDamage(attackComponent, entity.transform.position);
        }
        entity.StartCoroutine(AttackDelay());
        //ActionFinished();
    }

    private IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(2);
        ActionFinished();
    }

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
