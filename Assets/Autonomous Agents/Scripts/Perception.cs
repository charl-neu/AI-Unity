using UnityEngine;

public abstract class Perception : MonoBehaviour
{
    [SerializeField] string info;

    [SerializeField] protected string tagName;
    [SerializeField] protected LayerMask layerMask = Physics.AllLayers;
    [SerializeField, Range(0, 10)] protected float maxDistance = 5;
    [SerializeField, Range(0, 180)] protected float maxpathAngle = 180;

    [Header("Debug")]
    [SerializeField] protected bool debug;
    [SerializeField] protected Color debugColor = Color.green;

    public abstract GameObject[] GetGameObjects();

    public virtual GameObject GetGameObjectInDirection(Vector3 direction) { return null; }
    public virtual bool GetOpenDirection(ref Vector3 direction) { return false; }
}
