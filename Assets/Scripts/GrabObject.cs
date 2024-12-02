using UnityEngine;

public class GrabObject : MonoBehaviour
{
    public Camera cam;
    public Transform grabPosition;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
            Grab();
    }

    void Grab()
    {
        RaycastHit hitInfo;
        Ray ray = new(cam.transform.position, cam.transform.forward);

        if (!Physics.Raycast(ray, out hitInfo, 5f))
            return;

        if (!hitInfo.transform.CompareTag("Grabbable"))
            return;

        Debug.Log("Grabbed");
    }
}
