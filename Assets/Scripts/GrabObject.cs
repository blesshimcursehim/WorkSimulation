using UnityEngine;

public class GrabObject : MonoBehaviour
{
    public Camera camera;
    public Transform grabTransform;

    private Rigidbody grabbedRb = null;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
            if (grabbedRb != null)
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

        tempGrabbed.isKinematic = true;
        Debug.Log(grabbedRb);
        tempGrabbed.position = grabTransform.position;
        tempGrabbed.transform.parent = camera.transform;

        grabbedRb = tempGrabbed;
    }

    void Release()
    {

    }
}
