using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;
    private Animator animator;

    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public float mouseSensitivity = 100.0f;
    public float verticalLookLimit = 30.0f; // Limit for vertical look

    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0.0f;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the center of the screen
    }

    void Update()
    {
        // Get input for movement (WASD)
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Calculate movement direction
        moveDirection = new Vector3(moveHorizontal, 0.0f, moveVertical);
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= speed;

        // Check for jump (SPACE)
        if (Input.GetButton("Jump"))
        {
            moveDirection.y = jumpSpeed;
        }

        // Apply gravity smoothly
        moveDirection.y -= gravity * Time.deltaTime;

        // Move the character
        characterController.Move(moveDirection * Time.deltaTime);

        // Get mouse input for looking around
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotate the player around the Y axis
        transform.Rotate(Vector3.up * mouseX);

        // Rotate the camera around the X axis
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -verticalLookLimit, verticalLookLimit); // Clamp the vertical rotation to the specified limit
        Camera.main.transform.localRotation = Quaternion.Euler(rotationX, 0.0f, 0.0f);
    }
}