using UnityEngine;

public class Waypoint : NavNode
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Waypoint triggered by: " + other.gameObject.name);
        if (other.gameObject.TryGetComponent<NavAgent>(out NavAgent agent))
        {
            agent.TargetNode = neighbors[Random.Range(0, neighbors.Length)];
            Debug.Log("New target node assigned to agent: " + agent.TargetNode.name);
        }
        else         {
            Debug.Log("The object does not have a NavAgent component.");
        }
    }
}
