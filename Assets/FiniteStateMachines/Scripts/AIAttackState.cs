using UnityEngine;

public class AIAttackState : AIState
{
    public AIAttackState(StateAgent agent) : base(agent)
    {
    }

    public override void OnEnter()
    {
        agent.animator.SetTrigger("Attack");
        agent.movement.Destination = agent.transform.position; // Stop moving
        agent.timer = 1.0f;
        
        Attack();
    }

    public override void OnUpdate()
    {
        
        if (agent.timer <= 0)
        {
            agent.StateMachine.SetState<AIChaseState>();
        }
    }

    public override void OnExit()
    {
    }

    void Attack()
    {
        var colliders = Physics.OverlapSphere(agent.transform.position, 2.0f);

        foreach (var collider in colliders)
        {
            if (collider.gameObject.CompareTag(agent.tag)) continue;

            if (collider.gameObject.TryGetComponent<StateAgent>(out var stateAgent))
            {
                stateAgent.OnDamage(Random.Range(10, 20));
            }
        }
    }

}
