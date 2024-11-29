using UnityEngine;
//Ensures a CapsuleCollider and a Rigidbody component is attached to the GameObject
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]
public class LookFirstPersonCamera : MonoBehaviour
{
    // Sensitivity for mouse movement along the X- and Y-axis
    public float sensX = 500f;
    public float sensY = 500f;

    public Transform camera; // Reference to the camera Transform for rotation and positioning
    public float eyeHeight = 1f; // Height of the camera relative to the player

    // Private variables to track camera rotation on X- and Y-axis
    float xRotation;
    float yRotation;

    void Start()
    {
        // Locks the cursor to the center of the screen and hides it
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        // Sets the initial position of the camera to be at the player's eye height
        Vector3 cameraTargetposition = transform.position + (Vector3.up * eyeHeight);
        camera.position = cameraTargetposition;
    }

    void Update()
    {
        // Handles rotation of camera based on mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;
        // Update rotation values based on mouse input
        yRotation += mouseX;
        xRotation -= mouseY;
        // Clamp the vertical rotation to prevent over-rotation
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        // Apply the rotations to the player and the camera
        transform.eulerAngles = new Vector3(0f, yRotation, 0f);
        camera.eulerAngles = new Vector3(xRotation, yRotation, 0f);
        // Smoothly move the camera to the player's eye level position
        Vector3 cameraTargetPosition = transform.position + (Vector3.up * eyeHeight);
        camera.position = Vector3.Lerp(camera.position, cameraTargetPosition, 0.5f);
    }
}