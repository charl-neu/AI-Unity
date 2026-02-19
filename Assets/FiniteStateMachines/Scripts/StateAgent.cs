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
    public Animator animator;

    [Header("Parameters")]
    public float timer;
    public float health;
    public float maxhealth = 100.0f;
    public float distanceToDestination;
    public string stateName;
    public AIAgent enemy;

    //public AIStateMachine StateMachine { get; private set; } = new AIStateMachine();
    public PushdownStateMachine StateMachine { get; private set; } = new PushdownStateMachine();


    public Vector3 Destination
    {
        get { return movement.Destination; }
        set { movement.Destination = value; }
    }

    private void Start()
    {

        health = maxhealth;
        StateMachine.AddState(new AIIdleState(this));
        StateMachine.AddState(new AIPatrolState(this));
        StateMachine.AddState(new AIDeathState(this));
        StateMachine.AddState(new AIHitState(this));
        StateMachine.AddState(new AIAttackState(this));
        StateMachine.AddState(new AIChaseState(this));


        StateMachine.SetState<AIIdleState>();
    }

    private void Update()
    {
        UpdateParameters();
        StateMachine.Update();
        stateName = StateMachine.CurrentState?.Name;

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

    public void OnDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            StateMachine.SetState<AIDeathState>();
        }
        else
        {
            StateMachine.SetState<AIHitState>();
        }
    }

    private void DrawGUI()
    {

        // draw label of current state above agent
        GUI.backgroundColor = Color.black;
        GUI.skin.label.alignment = TextAnchor.MiddleCenter;
        Rect rect = new Rect(0, 0, 100, 20);
        // get point above agent
        Vector3 point = Camera.main.WorldToScreenPoint(transform.position);
        rect.x = point.x - (rect.width / 2);
        rect.y = Screen.height - point.y - rect.height - 20;
        // draw label with current state name
        GUI.Label(rect, StateMachine.CurrentState.Name);
    }
}
