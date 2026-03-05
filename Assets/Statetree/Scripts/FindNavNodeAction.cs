using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "FindNavNode", story: "[Self] finds [TargetNavnode]", category: "Action", id: "c9f53dfedda379d17146fee9152f1bdf")]
public partial class FindNavNodeAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<Transform> TargetNavnode;
    protected override Status OnStart()
    {
        TargetNavnode.Value = NavNode.GetRandomNavNode().transform;
        if (TargetNavnode.Value == null)
        {
            return Status.Failure;
        }

        return Status.Success;
    }

}

