using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FirstPersonController : MonoBehaviour
{
    [Header("Base Movement")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float lookSpeedX = 2f;
    [SerializeField] float lookSpeedY = 2f;
    [SerializeField] float upDownRange = 60f;
    [SerializeField] float jumpForce = 400f;

    [Header("Lean Limits")]
    [SerializeField] float minLeanAngle = -30f; // Minimum lean angle
    [SerializeField] float maxLeanAngle = 30f;  // Maximum lean angle

    [Header("Ground Check")]
    [SerializeField] float groundDist;
    [SerializeField] LayerMask groundLayers;

    private Rigidbody rb;
    private Camera playerCamera;
    private float rotationX = 0f;
    private float leanX = 0f; // New variable for lean
    Vector3 moveDir;
    bool isGrounded = false;
    bool canMove;

    private bool isInventoryOpen = false; // New variable to track inventory state

    void Start()
    {
        GameManager.Instance.Player = this.gameObject;

        rb = GetComponent<Rigidbody>();
        playerCamera = Camera.main;

        canMove = true;

        // Initially hide the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Toggle inventory and movement
        if (Input.GetKeyDown(KeyCode.I))
        {
            isInventoryOpen = !isInventoryOpen; // Toggle inventory state
            canMove = !isInventoryOpen; // Update CanMove based on inventory state
            UIManager.Instance.ToggleInventory(isInventoryOpen); // Toggle inventory UI

            // Update cursor visibility and lock state
            if (isInventoryOpen)
            {
                Cursor.lockState = CursorLockMode.None; // Unlock cursor
                Cursor.visible = true; // Show cursor
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked; // Lock cursor
                Cursor.visible = false; // Hide cursor
            }
        }

        if (canMove)
        {
            GroundCheck();
            HandleLookRotation();
            HandleMovement();
            HandleJump();
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = moveDir; // Use rb.velocity instead of rb.linearVelocity
    }

    #region Movement Handlers

    void GroundCheck()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundDist, groundLayers);
    }

    void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce);
        }
    }

    void HandleMovement()
    {
        moveDir.x = Input.GetAxis("Horizontal") * moveSpeed;
        moveDir.z = Input.GetAxis("Vertical") * moveSpeed;

        Vector3 moveClamp = transform.right * moveDir.x + transform.forward * moveDir.z;

        moveDir = Vector3.ClampMagnitude(moveClamp, moveSpeed);
        moveDir.y = rb.linearVelocity.y; // Use rb.velocity instead of rb.linearVelocity
    }

    void HandleLookRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * lookSpeedX;
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeedY;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -upDownRange, upDownRange);

        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);

        // Leaning logic
        leanX -= mouseY; // Adjust lean based on mouse Y input
        leanX = Mathf.Clamp(leanX, minLeanAngle, maxLeanAngle); // Clamp the lean angle

        // Apply the lean to the player's transform
        transform.localRotation = Quaternion.Euler(leanX, transform.localEulerAngles.y, 0);
        transform.Rotate(Vector3.up * mouseX);
    }
    #endregion
}