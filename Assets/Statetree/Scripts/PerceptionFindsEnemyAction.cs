using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "PerceptionFindsEnemy", story: "[Perception] finds [Enemy]", category: "Action", id: "273b5a94cf46a5852283e530682049f4")]
public partial class PerceptionFindsEnemyAction : Action
{
    [SerializeReference] public BlackboardVariable<Perception> Perception;
    [SerializeReference] public BlackboardVariable<GameObject> Enemy;
    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        var gameObjects = Perception.Value.GetGameObjects();
        Enemy.Value = (gameObjects.Length > 0) ? gameObjects[0] : null; 

        return (Enemy.Value != null) ? Status.Success : Status.Failure;
    }
    
}

