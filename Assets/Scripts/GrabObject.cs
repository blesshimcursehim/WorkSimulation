using UnityEngine;

public class GrabObject : MonoBehaviour
{
    //Public variables
    public Camera camera; // The camera used for raycasting to detect grabbable objects
    public Transform grabTransform; // The position in front of the camera where grabbed objects are held

    //Private variables
    public static GrabObject Instance { get; private set; }
    private Rigidbody grabbedRb = null; //Reference to the currently grabbed object's Rigidbody

    void Update()
    {
        // Press E to either grab an object if none is held, or release the currently held object.
        if (Input.GetKeyDown(KeyCode.E))
            if (grabbedRb == null)
                Grab();
            else
                Release();
    }

    private void Awake()
    {
        // Initialize the singleton instance. If one already exists, destroy this one
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }


    void Grab()
    {
        // Raycast forward from the camera to detect objects within 5 units
        RaycastHit hitInfo;
        Ray ray = new Ray(camera.transform.position, camera.transform.forward);

        // Perform the raycast
        if (Physics.Raycast(ray, out hitInfo, 5f))
        {
            // Check if the hit object has the "Grabbable" tag
            if (hitInfo.transform.CompareTag("Grabbable"))
            {
                // Get the attached Rigidbody component
                Rigidbody tempGrabbed = hitInfo.collider.attachedRigidbody;

                // Make sure we actually hit a Rigidbody
                if (tempGrabbed == null) return;

                // Assign tempGrabbed to grabbedRb
                grabbedRb = tempGrabbed;

                // Assign the grabbed object and set it to a held state.
                grabbedRb.isKinematic = true;
                grabbedRb.position = grabTransform.position;
                grabbedRb.transform.parent = camera.transform;
                grabbedRb.transform.localPosition = grabTransform.localPosition;
                grabbedRb.GetComponent<Collider>().enabled = false; // Disable the collider so it doesn't interfere with the player or environment.
            }
        }
    }

    void Release()
    {
        // Only release if an object is currently held.
        if (grabbedRb != null)
        {
            // Re-enable the object's collider so it can interact with the environment again.
            Collider grabbedCollider = grabbedRb.GetComponent<Collider>();
            if (grabbedCollider != null)
            {
                grabbedCollider.enabled = true; // Enable the collider when released
            }
            // Allow physics to affect the object again
            grabbedRb.isKinematic = false;
            grabbedRb.transform.parent = null;
            // Clear the reference to the released object
            grabbedRb = null;
        }
    }

    public void ForceReleaseIfHolding(GameObject obj)
    {
        // If the currently held object matches the given object, release it
        if (grabbedRb != null && grabbedRb.gameObject == obj)
        {
            Release();
        }
    }
}
