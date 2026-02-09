using UnityEngine;

[RequireComponent(typeof(NavPath))]
public class NavPathMovement : KinematicMovement
{
    NavPath navPath = null;
    public NavNode TargetNode { get; set; } = null;

    private void Awake()
    {
        navPath = GetComponent<NavPath>();
    }

    public override Vector3 Destination
    {
        get { return TargetNode.transform.position; }
        set { TargetNode = navPath.GeneratePath(transform.position, value); }
    }

    public void OnEnterNavNode(NavNode navNode)
    {
        if (navNode == TargetNode)
        {
            // get next nav node in path, returns null if no next
            TargetNode = navPath.GetNextNavNode(navNode);
        }
    }
}
