using UnityEngine;

public class AIPatrolState : AIState
{
    public AIPatrolState(StateAgent agent) : base(agent)
    {
    }

    public override void OnEnter()
    {
        agent.Destination = NavNode.GetRandomNavNode().transform.position;
    }

    public override void OnUpdate()
    {
        if (agent.distanceToDestination <= 0.5f)
        {
            // set state to idle
            agent.StateMachine.PopState();
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
