using UnityEngine;
using System.Collections.Generic;

public class NavAgent : AIAgent
{
    [SerializeField] NavPath navPath;
    [SerializeField] Movement movement;
    [SerializeField, Range(1,20)] float rotationSpeed = 5f;

    public NavNode TargetNode { get; set; } = null;

    void Start()
    {
        TargetNode = NavNode.GetNearestNavNode(transform.position);
        
        if (navPath != null)
        {
            TargetNode = navPath.GeneratePath(TargetNode.transform.position, TargetNode.transform.position);
        }
    }

    void Update()
    {
        if (TargetNode != null)
        {
           Vector3 direction = TargetNode.transform.position - transform.position;
           Vector3 force = direction.normalized * movement.maxForce;

           movement.ApplyForce(force);
        }

        if (movement.Velocity.magnitude > 0.01f)
        {
            //transform.LookAt(transform.position + movement.Velocity);
            var targetRotation = Quaternion.LookRotation(movement.Velocity);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }
    }

    public void OnEnterNavNode(NavNode navnode)
    {
        if (navnode != TargetNode) return;

        if (navPath != null)
        {

        }
        else
        {

            TargetNode = navnode.Neighbors[Random.Range(0, navnode.Neighbors.Count)];
        }
    }
}
