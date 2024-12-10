using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float lookSensitivity = 2f;
    [SerializeField] private float jumpForce = 2f;
    Rigidbody rb;
    GravityAttractor planet;
    [SerializeField] Transform cameraTransform;

    private void Start()
    {
        planet = GameObject.FindGameObjectWithTag("Planet").GetComponent<GravityAttractor>();
        rb = GetComponent<Rigidbody>();
        //cameraTransform = Camera.main.transform;
        Cursor.lockState = CursorLockMode.Locked;
    
    }

    private void FixedUpdate()
    {
        Move();
        Look();
        planet.Attract(transform, rb);
	if (Input.GetButton("Jump")) {
	    Jump();
	}
    }

    private void Move()
    {
        // Get input
        float horizontal = Input.GetAxis("Horizontal"); // A/D
        float vertical = Input.GetAxis("Vertical");   // W/S

        // Calculate local direction
        Vector3 moveDirection = transform.forward * vertical + transform.right * horizontal;

        // Apply movement
        transform.position += moveDirection.normalized * moveSpeed * Time.deltaTime;
    }

    private void Look()
    {
        // Mouse input
        float mouseX = Input.GetAxis("Mouse X") * lookSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * lookSensitivity;

        // Rotate the player horizontally (side-to-side)
        transform.Rotate(Vector3.up, mouseX, Space.Self);

        // Rotate the camera vertically (up/down)
        float currentXRotation = cameraTransform.localEulerAngles.x;
        float newXRotation = currentXRotation - mouseY;

        // Handle camera clamping (fix 360-degree wrap-around)
        if (newXRotation > 180f) newXRotation -= 360f;
        newXRotation = Mathf.Clamp(newXRotation, -90f, 90f);

        cameraTransform.localRotation = Quaternion.Euler(newXRotation, 0f, 0f);
    }

    private void Jump() 
    {
	rb.AddForce(transform.up * jumpForce);
	Debug.Log("Jumped");
    }


}
