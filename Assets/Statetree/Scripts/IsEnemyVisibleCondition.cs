using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "isEnemyVisible", story: "[Enemy] is Visible", category: "Conditions", id: "9a15cd886d7ea2b2e9047e5f50faf843")]
public partial class IsEnemyVisibleCondition : Condition
{
    [SerializeReference] public BlackboardVariable<GameObject> Enemy;

    public override bool IsTrue()
    {
        return Enemy.Value != null;
    }

    public override void OnStart()
    {
    }

    public override void OnEnd()
    {
    }
}
