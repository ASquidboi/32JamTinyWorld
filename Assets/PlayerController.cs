using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float lookSensitivity = 2f;
    [SerializeField] private float jumpForce = 2f;
    Rigidbody rb;
    GravityAttractor planet;
    [SerializeField] Transform cameraTransform;
    [SerializeField] JumpHitbox jumpHitbox;


    public float powerLvl = 0f;
    


    public float radius = 5.0F;
    public float power = 10.0F;
    bool canHonk = false;
    float honkLvl = 0f;

    //Honk at 5
    //High jump at 10
    //Speed at 15
    //Powerful honk at 25
    //Fly at like 50 or something
    


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
	    
    }
    private void Update()
    {
        if (Input.GetButtonDown("Jump") && jumpHitbox.CanJump == true)
        {
            Jump();
        }
        if (Input.GetButtonDown("Fire") && canHonk == true)
        {
            Honk();
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

    void Honk()
    {
        Debug.Log("Honk");
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        if (honkLvl == 1)
        {
            
        }
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (hit.tag != "Player")
            {
                if (rb != null)
                {
                    rb.AddExplosionForce(power, explosionPos, radius, 3.0F);
                    Debug.Log("Honked");
                }
            }
        }
    }

    public void ConsumeItem(float val)
    {
        powerLvl += val;
        if (powerLvl >= 5)
        {
            canHonk = true;
        }
        if (powerLvl >= 25)
        {
            honkLvl = 1;
            if (honkLvl == 0)
            {
                power = power * 2;
                radius = radius * 2;
            }
        }
    }

}
