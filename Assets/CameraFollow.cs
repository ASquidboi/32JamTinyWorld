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

    public float explosionRadius = 5f;
    public float explosionForce = 700f;
    public Vector3 explosionOffset = Vector3.zero;

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
    

    // Call this method to trigger the explosion
    public void Explode()
    {
        Vector3 explosionPosition = transform.position + explosionOffset;

        // Find all colliders within the explosion radius
        Collider[] colliders = Physics.OverlapSphere(explosionPosition, explosionRadius);

        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();

            if (rb != null)
            {
                // Apply explosion force
                rb.AddExplosionForce(explosionForce, explosionPosition, explosionRadius);
            }
        }
    }

    // Optional: Visualize the explosion radius in the editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + explosionOffset, explosionRadius);
    }
}