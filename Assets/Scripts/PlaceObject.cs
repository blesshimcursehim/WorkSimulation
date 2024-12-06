using UnityEngine;

public class PlaceObject : MonoBehaviour
{
    public void Place(Rigidbody grabbedRb, float maxDist)
    {
        GameObject[] placementPoints = GameObject.FindGameObjectsWithTag("PlacementPoints");

        //Get the closest point for placement
        GameObject closestPlacementPoint = null;
        float closestDist = Mathf.Infinity;

        foreach (GameObject placement in placementPoints)
        {
            float placeDist = Vector3.Distance(grabbedRb.position, placement.transform.position);

            if (placeDist < closestDist)
            {
                closestPlacementPoint = placement;
                closestDist = placeDist;
            }
        }

        if (closestDist > maxDist) //Not able to reach any placement point
            return;

        grabbedRb.isKinematic = true;
        grabbedRb.transform.parent = closestPlacementPoint.transform;
        grabbedRb.transform.localEulerAngles = Vector3.zero;
        grabbedRb.transform.localPosition = Vector3.zero;
    }
}
