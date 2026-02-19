using System.Collections.Generic;
using UnityEngine;

public class PushdownStateMachine 
{
    Dictionary<string, AIState> states = new Dictionary<string, AIState>();

    Stack<AIState> stateStack = new Stack<AIState>();
    public AIState CurrentState { get { return (stateStack.Count > 0) ? stateStack.Peek() : null; } }

    public void Update()
    {
        CurrentState?.OnUpdate();
    }

    public void PushState(string name)
    {
        if (!states.ContainsKey(name))
        {
            Debug.LogError($"State '{name}' does not exist in the state machine.");
            return;
        }

        var nextState = states[name];
        CurrentState?.OnExit();
        stateStack.Push(nextState);
        CurrentState?.OnEnter();
    }

    public void PushState<T>()
    {
        PushState(typeof(T).Name);
    }

    public void PopState()
    {
        Debug.Log(stateStack);
        if (stateStack.Count > 0)
        {
            // Exit the current state
            CurrentState?.OnExit();
            stateStack.Pop();
            // Enter the new current state
            CurrentState?.OnEnter();
            //CurrentState?.OnUpdate();
        }
    }



    public void SetState(string name)
    {
        if (!states.ContainsKey(name))
        {
            Debug.LogError($"State '{name}' does not exist in the state machine.");
            return;
        }

        while (stateStack.Count > 0)
        {
            // Exit the current state
            CurrentState?.OnExit();
            stateStack.Pop();
        }

        var newState = states[name];
        // Enter the new state
        stateStack.Push(newState);
        CurrentState?.OnEnter();

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

    public string GetString()
    {
        string str = "";

        var array = stateStack.ToArray();
        for (int i = 0; i < array.Length; i++)
        {
            str += array[i].Name;
            if (i < array.Length - 1) str += "\n";
        }

        return str;
    }

    
}
