using UnityEngine;

public class GrabObject : MonoBehaviour
{
    public Camera camera;
    public Transform grabTransform;
    public PlaceObject placeObjectComponent;

    private Rigidbody grabbedRb = null;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
            if (grabbedRb == null)
                Grab();
            else
                Release();
    }

    void Grab()
    {
        RaycastHit hitInfo;
        Ray ray = new Ray(camera.transform.position, camera.transform.forward);

        if (!Physics.Raycast(ray, out hitInfo, 5f))
            return;

        if (!hitInfo.transform.CompareTag("Grabbable"))
            return;

        Rigidbody tempGrabbed = hitInfo.collider.attachedRigidbody;

        grabbedRb.isKinematic = true;
        grabbedRb.position = grabTransform.position;
        grabbedRb.transform.parent = camera.transform;
        grabbedRb.transform.localPosition = grabTransform.localPosition;
        grabbedRb.GetComponent<Collider>().enabled = false;
    }

    void Release()
    {
        grabbedRb.GetComponent<Collider>().enabled = true;
        grabbedRb.isKinematic = false;
        grabbedRb.transform.parent = null;


        if (placeObjectComponent != null)
            placeObjectComponent.Place(grabbedRb, 3f);

        grabbedRb = null;
    }
}
