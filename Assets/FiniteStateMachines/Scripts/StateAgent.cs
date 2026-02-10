using UnityEngine;

public class StateAgent : AIAgent
{
    enum State
    {
        Idle,
        Patrol,
        Chase,
        Flee,
        Attack,
        Death
    }

    [SerializeField] Movement movement;
    [SerializeField] Perception perception;

    [SerializeField] State state;
    [Header("Parameters")]
    [SerializeField] float timer;
    [SerializeField] float distanceToDestination;
    [SerializeField] AIAgent enemy;


    public Vector3 Destination
    {
        get { return movement.Destination; }
        set { movement.Destination = value; }
    }

    private void Start()
    {
        state = State.Idle;
        timer = 2.0f;
    }

    private void Update()
    {
        // update parameters
        timer -= Time.deltaTime;
        distanceToDestination = Vector3.Distance(transform.position, Destination);
        // look for enemies
        var gameObjects = perception.GetGameObjects();
        if (gameObjects.Length > 0)
        {
            gameObjects[0].TryGetComponent<AIAgent>(out enemy);
        }
        else
        {
            enemy = null;
        }

        switch (state)
        {
            case State.Idle:
                if (timer <= 0)
                {
                    state = State.Patrol;
                    Destination = NavNode.GetRandomNavNode().transform.position;
                }
                if (enemy != null)
                {
                    state = State.Chase;
                }
                break;
            case State.Patrol:
                if (distanceToDestination <= 0.5f)
                {
                    state = State.Idle;
                    timer = Random.Range(2.0f, 4.0f);
                }
                if (enemy != null)
                {
                    state = State.Chase;
                }
                break;
            case State.Chase:
                if (enemy != null)
                {
                    Destination = enemy.transform.position;
                }
                else
                {
                    state = State.Idle;
                    timer = Random.Range(1.0f, 2.0f);
                }
                break;
            case State.Flee:
                break;
            case State.Attack:
                break;
            case State.Death:
                break;
            default:
                break;
        }
    }
}
