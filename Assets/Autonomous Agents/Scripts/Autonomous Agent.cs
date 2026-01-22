using UnityEngine;

public class AutonomousAgent : AIAgent

{

    [SerializeField] Movement movement;
    [SerializeField] Perception seekperception;
    [SerializeField] Perception fleeperception;
    [SerializeField] Perception flockperception;

    [Header("Wander")]
    [SerializeField] float wanderRadius = 1;
    [SerializeField] float wanderDistance = 1;
    [SerializeField] float wanderDisplacement = 1;

    [Header("Flock")]
    [SerializeField, Range(0, 5)] float cohesionWeight = 1;
    [SerializeField, Range(0, 5)] float separationWeight = 1;
    [SerializeField, Range(0, 5)] float alignmentWeight = 1;
    [SerializeField, Range(0, 5)] float separationRadius = 1;

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

        if (flockperception != null)
        {
            var gameObjects = flockperception.GetGameObjects();
            if (gameObjects.Length > 0)
            {
                hasTarget = true;
                movement.ApplyForce(Cohesion(gameObjects) * cohesionWeight);
                movement.ApplyForce(Separation(gameObjects, separationRadius) * separationWeight);
                movement.ApplyForce(Alignment(gameObjects) * alignmentWeight);
            }
        }

        if (!hasTarget)
        {
            //use getting started force in combination with random wandering if the agent is not moving. we want to avoid agents doing nothing.
            Vector3 force = Wander();
            movement.ApplyForce(force);
        }

        transform.position = Utilities.Wrap(transform.position, new Vector3(-25,0,-25), new Vector3(25,0,25));
        
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

    private Vector3 Cohesion(GameObject[] neighbors)
    {
        Vector3 positions = Vector3.zero;
        // accumulate the position vectors of the neighbors
        foreach (var neighbor in neighbors)
        {
            positions += neighbor.transform.position;

        }

        // average the positions to get the center of the neighbors
        Vector3 center = positions / neighbors.Length;
        // create direction vector to point towards the center of the neighbors from agent position
        Vector3 direction = center - transform.position;

        // steer towards the center point
        Vector3 force = GetSteeringForce(direction);


        return force;
    }



    private Vector3 Separation(GameObject[] neighbors, float radius)
    {
        Vector3 separation = Vector3.zero;
        // accumulate the separation vectors of the neighbors
        foreach (var neighbor in neighbors)
        {
            // get direction vector away from neighbor
            Vector3 direction = transform.position - neighbor.transform.position;
            float distance = direction.magnitude;
            // check if within separation radius
            if (distance > 0 || distance < radius)
		    {
                // scale separation vector inversely proportional to the direction distance
                // closer the distance the stronger the separation
                separation += direction * (1 / distance);
            }
        }

        // steer towards the separation point
        Vector3 force = (separation != Vector3.zero) ? GetSteeringForce(separation) : Vector3.zero;

        return force;
    }

    private Vector3 Alignment(GameObject[] neighbors)
    {
        Vector3 velocities = Vector3.zero;
        // accumulate the velocity vectors of the neighbors
        foreach (var neighbor in neighbors)
	    {
            // get the velocity from the agent movement
            if (neighbor.TryGetComponent<AutonomousAgent>(out AutonomousAgent agent))
            {
                // add agent movement velocity to velocities
                velocities += agent.movement.Velocity;
            }
        }
        // get the average velocity of the neighbors
        Vector3 averageVelocity = velocities / neighbors.Length;

        // steer towards the average velocity
        Vector3 force = GetSteeringForce(averageVelocity);


        return force;
    }


}