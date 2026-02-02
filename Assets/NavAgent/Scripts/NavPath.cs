using UnityEngine;
using System.Collections.Generic;

public class NavPath : MonoBehaviour
{
    List<NavNode> path = new List<NavNode>();

    public NavNode GeneratePath(Vector3 startPosition, Vector3 endPosition)
    {
        var startNode = NavNode.GetNearestNavNode(startPosition);
        var endNode = NavNode.GetNearestNavNode(endPosition);

        path.Clear();
        NavNode.ResetNavNodes();

        //generate path

        return startNode;
    }
}
