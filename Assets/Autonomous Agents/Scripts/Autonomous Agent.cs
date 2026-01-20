using UnityEngine;

public class AutonomousAgent : AIAgent

{

    [SerializeField] Movement movement;
    [SerializeField] Perception seekperception;
    [SerializeField] Perception fleeperception;


    void Start()
    {

    }

    void Update()
    {
        if (seekperception != null)
        {
            var gameObjects = seekperception.GetGameObjects();
            if (gameObjects.Length > 0)
            {
                Vector3 force = Seek(gameObjects[0]);
                movement.ApplyForce(force);
            }
        }
        if (fleeperception != null)
        {
            var gameObjects = fleeperception.GetGameObjects();
            if (gameObjects.Length > 0)
            {
                Vector3 force = Flee(gameObjects[0]);
                movement.ApplyForce(force);
            }
        }
        //foreach (var obj in gameObjects)
        //{
        //    Debug.DrawLine(transform.position, obj.transform.position, Color.red);
        //}



        transform.position = Utilities.Wrap(transform.position, new Vector3(-25,-25,-25), new Vector3(25,25,25));

        if (movement.Velocity.sqrMagnitude == 0)
        {
            //use getting started force in combination with random wandering if the agent is not moving. we want to avoid agents doing nothing.
            Vector3 randomDirection = Quaternion.Euler(0, Random.Range(-15f, 15f), 0) * transform.forward; // random direction in xz plane facing mostly in the direction the agent is facing
            Vector3 direction = Vector3.Lerp(movement.Velocity.normalized, randomDirection.normalized, 0.3f).normalized * movement.maxSpeed; // slightly adjust current velocity towards random direction
            Vector3 force = GetStartingForce(direction);
            movement.ApplyForce(force);
        }

        transform.rotation = Quaternion.LookRotation(movement.Velocity);
    }

    Vector3 Seek(GameObject go)
    {
        Vector3 direction = go.transform.position - transform.position;
        Vector3 force = GetStartingForce(direction);
        return force;
    }

    Vector3 Flee(GameObject go)
    {
        Vector3 direction = transform.position - go.transform.position;
        Vector3 force = GetStartingForce(direction);
        return force;
    }

    Vector3 GetStartingForce(Vector3 direction)
    {
        Vector3 desired = direction.normalized * movement.maxSpeed;
        Vector3 steer = desired - movement.Velocity;
        Vector3 force = Vector3.ClampMagnitude(steer, movement.maxForce);
        return force;
    }

}