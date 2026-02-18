using UnityEngine;

public class AIIdleState : AIState
{
    public AIIdleState(StateAgent agent) : base(agent)
    {
    }

    public override void OnEnter()
    {
        agent.timer = 2.0f;
    }

    public override void OnUpdate()
    {
        if (agent.timer <= 0)
        {
            // transition to patrol
            agent.StateMachine.PushState<AIPatrolState>();
        }

        if (agent.enemy != null)
        {
            // set state to chase
            agent.StateMachine.PushState<AIChaseState>();
        }
    }

    public override void OnExit()
    {
        
    }

}
