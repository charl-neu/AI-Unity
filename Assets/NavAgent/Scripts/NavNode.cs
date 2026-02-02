using System.Collections.Generic;
using UnityEngine;

public class NavNode : MonoBehaviour
{
    [SerializeField] protected List<NavNode> neighbors;

    public List<NavNode> Neighbors { get { return neighbors; } }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<NavAgent>(out NavAgent navAgent))
        {
            navAgent.OnEnterNavNode(this);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.TryGetComponent<NavAgent>(out NavAgent navAgent))
        {
            navAgent.OnEnterNavNode(this);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        foreach (var neighbor in neighbors)
        {
            Gizmos.DrawLine(transform.position, neighbor.transform.position);
        }
    }

    #region helper_functions
    public static NavNode[] GetAllNavNodes()
    {
        // return all objects of type nav node
        return FindObjectsByType<NavNode>(FindObjectsSortMode.None);
    }

    public static NavNode GetRandomNavNode()
    {
        var navNodes = GetAllNavNodes();
        return (navNodes.Length == 0) ? null : navNodes[Random.Range(0, navNodes.Length)];
    }

    public static NavNode GetNearestNavNode(Vector3 position)
    {
        NavNode nearestNavNode = null;
        var navNodes = GetAllNavNodes();
        float nearestDistance = float.MaxValue;
        foreach (var navNode in navNodes)
        {
            float distance = Vector3.Distance(position, navNode.transform.position);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestNavNode = navNode;
            }
        }

        return nearestNavNode;
    }
    #endregion
}
