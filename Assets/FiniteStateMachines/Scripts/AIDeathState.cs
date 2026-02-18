using UnityEngine;

public class AIDeathState : AIState
{
    public AIDeathState(StateAgent agent) : base(agent)
    {
    }

    public override void OnEnter()
    {
        agent.animator.SetTrigger("Death");
        agent.movement.Destination = agent.transform.position; // Stop movement

        agent.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false; // Disable NavMeshAgent to prevent movement)
        GameObject.Destroy(agent.gameObject, 5f);
    }

    public override void OnUpdate()
    {
          
    }

    public override void OnExit()
    {
        
    }

}
