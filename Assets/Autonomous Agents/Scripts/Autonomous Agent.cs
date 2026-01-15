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

        transform.position = Utilities.Wrap(transform.position, new Vector3(-15,-15,-15), new Vector3(15,15,15));

        if (movement.Velocity.sqrMagnitude != 0)
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