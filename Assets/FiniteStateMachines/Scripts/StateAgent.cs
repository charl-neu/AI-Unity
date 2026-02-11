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

    public Movement movement;
    public Perception perception;

    [SerializeField] State state;
    [Header("Parameters")]
    public float timer;
    public float distanceToDestination;
    public AIAgent enemy;

    public AIStateMachine StateMachine { get; private set; } = new AIStateMachine();


    public Vector3 Destination
    {
        get { return movement.Destination; }
        set { movement.Destination = value; }
    }

    private void Start()
    {
        state = State.Idle;
        timer = 2.0f;

        StateMachine.AddState(new AIIdleState(this));
        StateMachine.AddState(new AIPatrolState(this));

        StateMachine.SetState<AIIdleState>();
    }

    private void Update()
    {
        UpdateParameters();
        StateMachine.Update();

        
    }

    private void UpdateParameters()
    {
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
    }
}
