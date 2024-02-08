using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AISphereCastPerception1 : AIPerception
{
    [SerializeField] private float radius = 0.5f;
    [SerializeField] private int rayCount = 2;
    public override GameObject[] GetGameObjects()
    {
        List<GameObject> result = new List<GameObject>();

        Vector3[] directions = Utilities.GetDirectionsInCircle(rayCount, maxAngle);
        foreach (Vector3 direction in directions)
        {
            Ray ray = new Ray(transform.position, transform.rotation * direction);
            if (Physics.SphereCast(ray, radius, out RaycastHit hitInfo, distance))
            {
                Debug.DrawRay(ray.origin, ray.direction * hitInfo.distance, Color.red);
                if (hitInfo.collider.gameObject == gameObject) continue;
                if (tagName == "" || hitInfo.collider.CompareTag(tagName))
                {
                    result.Add(hitInfo.collider.gameObject);
                }
            }
            else Debug.DrawRay(ray.origin, ray.direction * distance, Color.green);
        }
        result = result.Distinct().ToList();
        return result.ToArray();
    }
    public bool GetOpenDirection(ref Vector3 openDirection)
    {
        Vector3[] directions = Utilities.GetDirectionsInCircle(rayCount, maxAngle);
        foreach (var direction in directions)
        {
            // cast ray from transform position towards direction (use game object orientation)
            Ray ray = new Ray(transform.position, transform.rotation * direction);
            // if there is NO raycast hit then that is an open direction
            if (!Physics.SphereCast(ray, radius, out RaycastHit raycastHit, distance, layerMask))
            {
                Debug.DrawRay(ray.origin, ray.direction * distance, Color.green);
                // set open direction
                openDirection = ray.direction;
                return true;
            }
        }

        // no open direction
        return false;
    }

    public bool CheckDirection(Vector3 direction)
    {
        // create ray in direction (use game object orientation)
        Ray ray = new Ray(transform.position, transform.rotation * direction);
        // check ray cast
        return Physics.SphereCast(ray, radius, distance, layerMask);

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Vector3[] directions = Utilities.GetDirectionsInCircle(rayCount, maxAngle);
        foreach (Vector3 direction in directions)
        {
            Ray ray = new Ray(transform.position, transform.rotation * direction);
            Gizmos.DrawWireSphere(ray.origin + ray.direction * distance, radius);
        }

    }
}
