using System.Collections.Generic;
using UnityEngine;

public class AIStateMachine 
{
    Dictionary<string, AIState> states = new Dictionary<string, AIState>();

    public AIState CurrentState { get; private set; }

    public void Update()
    {
        CurrentState?.OnUpdate();
    }

    public string GetString()
    {
        return (CurrentState != null) ? CurrentState.Name : "No State";
    }

    public void SetState(string name)
    {
        if (!states.ContainsKey(name))
        {
            Debug.LogError($"State '{name}' does not exist in the state machine.");
            return;
        }

        var nextState = states[name];
        if (nextState == CurrentState)
        {
            return; // Already in the desired state
        }

        // Exit the current state
        CurrentState?.OnExit();

        // Enter the new state
        CurrentState = nextState;
        CurrentState?.OnEnter();
        CurrentState?.OnUpdate();

    }

    public void SetState<T>()
    {
        SetState(typeof(T).Name);
    }

    public void AddState(AIState state)
    {
        if (states.ContainsKey(state.Name))
        {
            Debug.LogError($"State '{state.Name}' already exists in the state machine.");
            return;
        }

        states[state.Name] = state;
    }
}
