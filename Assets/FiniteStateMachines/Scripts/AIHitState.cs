using UnityEngine;

public class AIHitState : AIState
{
    public AIHitState(StateAgent agent) : base(agent)
    {
    }

    public override void OnEnter()
    {
        agent.timer = 1.0f;
        agent.animator.SetTrigger("Hit");
        agent.movement.Destination = agent.transform.position; // stop moving
    }

    public override void OnUpdate()
    {
        if (agent.timer <= 0)
        {
            // transition to patrol
            agent.StateMachine.SetState<AIPatrolState>();
        }
    }

    public override void OnExit()
    {
        
    }

}
