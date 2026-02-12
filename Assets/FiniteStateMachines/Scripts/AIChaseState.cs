using UnityEngine;

public class AIChaseState : AIState
{
    public AIChaseState(StateAgent agent) : base(agent)
    {
    }

    public override void OnEnter()
    {
        agent.movement.Speed *= 2;
    }

    public override void OnUpdate()
    {
        if (agent.enemy != null)
        {
            agent.movement.Destination = agent.enemy.transform.position;
            if (agent.distanceToDestination <= 1.5f)
            {
                agent.StateMachine.SetState<AIAttackState>();
            }
        } else
        {
            agent.StateMachine.SetState<AIIdleState>();
        }
    }

    public override void OnExit()
    {
        agent.movement.Speed /= 2;
    }

}
