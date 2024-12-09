using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //Dummy comment?
    public Transform player;          // The player’s transform to follow
    public float rotationSpeed = 3.0f; // Rotation speed of the camera
    public float pitchMin = -30f;     // Min vertical angle (looking up/down)
    public float pitchMax = 60f;      // Max vertical angle (looking up/down)

    private float yaw = 0.0f;         // Horizontal rotation angle
    private float pitch = 0.0f;       // Vertical rotation angle

    public float Power = 0f;

    void Update()
    {
        // Get mouse input for camera rotation
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Update yaw and pitch based on mouse movement
        yaw += mouseX * rotationSpeed;
        pitch -= mouseY * rotationSpeed;

        // Clamp the pitch to prevent full 360-degree vertical rotation
        pitch = Mathf.Clamp(pitch, pitchMin, pitchMax);

        // Apply the rotation to the camera
        transform.rotation = Quaternion.Euler(pitch, yaw, 0);

        // Optionally, lock the cursor to the center of the screen (to prevent it from leaving the window)
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //transform.position = player.transform.position + offset;
    }
}