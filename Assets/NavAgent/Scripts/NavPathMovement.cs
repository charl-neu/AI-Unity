using UnityEngine;

[RequireComponent(typeof(NavPath))]
public class NavPathMovement : KinematicMovement
{
    NavPath navPath = null;
    public NavNode TargetNode { get; set; } = null;

    private void Awake()
    {
        navPath = GetComponent<NavPath>();
        GetNewPath();
    }

    public override Vector3 Destination
    {
        get { return TargetNode.transform.position; }
        set { TargetNode = navPath.GeneratePath(transform.position, value); }
    }

    public void Update()
    {
        if (TargetNode != null)
        {
            Vector3 direction = Destination - transform.position;
            direction.Normalize();
            ApplyForce(direction);
        }
    }

    public void OnEnterNavNode(NavNode navNode)
    {
        if (navNode == TargetNode)
        {
            // get next nav node in path, returns null if no next
            TargetNode = navPath.GetNextNavNode(navNode);
            if (TargetNode == null)
            {
                // reached end of path, get a new one
                GetNewPath();
            }
        }
    }

    public void GetNewPath()
    {
        NavNode startingnode = NavNode.GetNearestNavNode(transform.position);
        NavNode destinationNode = NavNode.GetRandomNavNode();
        TargetNode = navPath.GeneratePath(startingnode, destinationNode);
    }
}
