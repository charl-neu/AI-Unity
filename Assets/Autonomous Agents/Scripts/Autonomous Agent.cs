using UnityEngine;

public class AutonomousAgent : AIAgent

{

    [SerializeField] Movement movement;
    [SerializeField] Perception seekperception;
    [SerializeField] Perception fleeperception;

    [Header("Wander")]
    [SerializeField] float wanderRadius = 1;
    [SerializeField] float wanderDistance = 1;
    [SerializeField] float wanderDisplacement = 1;



    float wanderAngle = 0.0f;


    void Start()
    {
        wanderAngle = Random.Range(0, 360);
    }

    void Update()
    {
        bool hasTarget = false;

        if (seekperception != null)
        {
            var gameObjects = seekperception.GetGameObjects();
            if (gameObjects.Length > 0)
            {
                hasTarget = true;
                Vector3 force = Seek(gameObjects[0]);
                movement.ApplyForce(force);
            }
        }
        if (fleeperception != null)
        {
            var gameObjects = fleeperception.GetGameObjects();
            if (gameObjects.Length > 0)
            {
                hasTarget = true;
                Vector3 force = Flee(gameObjects[0]);
                movement.ApplyForce(force);
            }
        }
        //foreach (var obj in gameObjects)
        //{
        //    Debug.DrawLine(transform.position, obj.transform.position, Color.red);
        //}

        if (!hasTarget)
        {
            //use getting started force in combination with random wandering if the agent is not moving. we want to avoid agents doing nothing.
            Vector3 force = Wander();
            movement.ApplyForce(force);
        }

        transform.position = Utilities.Wrap(transform.position, new Vector3(-25,-25,-25), new Vector3(25,25,25));
        
        transform.rotation = Quaternion.LookRotation(movement.Velocity);
    }

    Vector3 Seek(GameObject go)
    {
        Vector3 direction = go.transform.position - transform.position;
        Vector3 force = GetSteeringForce(direction);
        return force;
    }

    Vector3 Flee(GameObject go)
    {
        Vector3 direction = transform.position - go.transform.position;
        Vector3 force = GetSteeringForce(direction);
        return force;
    }

    Vector3 GetSteeringForce(Vector3 direction)
    {
        Vector3 desired = direction.normalized * movement.maxSpeed;
        Vector3 steer = desired - movement.Velocity;
        Vector3 force = Vector3.ClampMagnitude(steer, movement.maxForce);
        return force;
    }

    private Vector3 Wander()

    {

        // randomly adjust the wander angle within (+/-) displacement range 

        wanderAngle += Random.Range(-wanderDisplacement, wanderDisplacement);

        // calculate a point on the wander circle using the wander angle 

        Quaternion rotation = Quaternion.AngleAxis(wanderAngle, Vector3.up);

        Vector3 pointOnCircle = rotation * Vector3.forward * wanderRadius;

        // project the wander circle in front of the agent 

        Vector3 circleCenter = movement.Velocity.normalized * wanderDistance;

        // steer toward the target point (circle center + point on circle) 

        Vector3 force = GetSteeringForce(circleCenter + pointOnCircle);

        return force;

    }
}