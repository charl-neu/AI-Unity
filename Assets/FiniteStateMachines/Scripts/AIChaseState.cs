using UnityEngine;

public class AIChaseState : AIState
{
    public AIChaseState(StateAgent agent) : base(agent)
    {
    }

    public override void OnEnter()
    {
        agent.movement.Velocity *= 2;
    }

    public override void OnUpdate()
    {
        if (agent.enemy != null)
        {
            agent.movement.Destination = agent.enemy.transform.position;
            if (agent.distanceToEnemy <= 1.0f)
            {
                agent.StateMachine.PushState<AIAttackState>();
            }
        } else
        {
            agent.StateMachine.PopState();
        }
    }

    public override void OnExit()
    {
        agent.movement.Velocity /= 2;
    }

}
