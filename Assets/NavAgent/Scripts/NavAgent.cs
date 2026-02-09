using UnityEngine;
using System.Collections.Generic;

public class NavAgent : AIAgent
{
    [SerializeField] Movement movement;

    public Vector3 Destination
    {
        get { return movement.Destination; }
        set { movement.Destination = value; }
    }

}
