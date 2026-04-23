using UnityEngine;
public class Mechanics : MonoBehaviour
{
    public static GameObject FindClosestGameObjectWithTag(Vector3 currentPosition, string tag)
    {
        // Search ALL objects with this tag
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(tag);

        GameObject closestObject = null;
        float closestDistance = Mathf.Infinity;
    
        foreach (GameObject candidate in objectsWithTag)
        {
            // Distance calculation
            float distance = Vector3.Distance(currentPosition, candidate.transform.position);

            // If closer than the last one, become the closestObject
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestObject = candidate;
            }
        }
    
        return closestObject;
    }
}