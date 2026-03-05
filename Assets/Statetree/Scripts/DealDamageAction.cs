using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "DealDamage", story: "Deal damage to [Enemy]", category: "Action", id: "cc122e969364a8c6cd80d6da0df99167")]
public partial class DealDamageAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Enemy;

    protected override Status OnStart()
    {
        if (Enemy.Value == null) return Status.Failure;

        var agent = Enemy.Value.GetComponent<BehaviorGraphAgent>();

        if (agent == null) return Status.Failure;
        agent.BlackboardReference.GetVariableValue<float>("Health", out float health);

        health -= 30.0f;
        Debug.Log($"health: {health}");
        agent.BlackboardReference.SetVariableValue<float>("Health", health);

        return Status.Success;
    }
}

