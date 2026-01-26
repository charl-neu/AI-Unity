using UnityEngine;
using System.Collections.Generic;

public class RaycastPerception : Perception
{
    [SerializeField, Tooltip("The number of rays casted."), Range(1,20)] int numRays = 1;

    public override GameObject[] GetGameObjects()
    {
        // create result list
        List<GameObject> result = new List<GameObject>();

        // get array of directions in circle
        Vector3[] directions = Utilities.GetDirectionsInCircle(numRays, maxpathAngle);

        // iterate through directions
        foreach (var direction in directions)
        {
            GameObject go = GetGameObjectInDirection(transform.rotation * direction);
            if (go != null && !result.Contains(go))
            {
                result.Add(go);
            }
        }

        // convert list to array
        return result.ToArray();
    }

    public override GameObject GetGameObjectInDirection(Vector3 direction) 
    {
        Ray ray = new Ray(transform.position,  direction);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, maxDistance, layerMask))
        {
            // do not include ourselves
            if (raycastHit.collider.gameObject == gameObject) return null;
            // check for matching tag
            if (tagName == "" || raycastHit.collider.CompareTag(tagName))
            {
                if (debug) Debug.DrawRay(ray.origin, ray.direction * raycastHit.distance, Color.red);
                return raycastHit.collider.gameObject;
            }
            else
            {
                if (debug) Debug.DrawRay(ray.origin, ray.direction * raycastHit.distance, Color.yellow);
            }
        }
        else
        {
            if (debug) Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.green);
        }
        return null;
    }

    public override bool GetOpenDirection(ref Vector3 openDirection) 
    {
        // get array of directions in circle
        Vector3[] directions = Utilities.GetDirectionsInCircle(numRays, maxpathAngle);

        // iterate through directions
        foreach (var direction in directions)
        {
            GameObject go = GetGameObjectInDirection(transform.rotation * direction);
            if (go == null)
            {
                openDirection = transform.rotation * direction;
                return true;
            }
        }

        return false;
    }

    private void OnDrawGizmos()
    {
        if (!debug) return;
        Vector3[] directions = Utilities.GetDirectionsInCircle(numRays, maxpathAngle);
        foreach (var direction in directions)
        {
            Gizmos.color = debugColor;
            Gizmos.DrawRay(transform.position, transform.rotation * direction * maxDistance);
        }
    }
}
