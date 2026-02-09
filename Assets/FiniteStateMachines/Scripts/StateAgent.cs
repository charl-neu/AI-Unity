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
        Dead
    }

    [SerializeField] Movement movement;
    [SerializeField] State state;
    [Header("Parameters")]
    [SerializeField] float timer;
    [SerializeField] float distanceToDestination;

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
        timer -= Time.deltaTime;
        distanceToDestination = Vector3.Distance(transform.position, Destination);

        switch (state)
        {
            case State.Idle:
                if (timer < 0)
                {
                    state = State.Patrol;
                    Destination = NavNode.GetRandomNavNode().transform.position;
                }
                break;
            case State.Patrol:
                if (distanceToDestination < 0.5)
                {
                    state = State.Patrol;
                    timer = Random.Range(0.0f, 2.0f);
                }
                break;
            case State.Chase:
                break;
            case State.Flee:
                break;
            case State.Attack:
                break;
            case State.Dead:
                break;
            default:
                break;
        }
    }
}
