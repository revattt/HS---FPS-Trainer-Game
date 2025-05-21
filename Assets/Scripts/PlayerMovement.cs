using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float sprintSpeed = 10f;
    public float jumpHeight = 1.5f;
    public float gravity = -9.81f;
    public float valorantSens = 0.24f;
    public float baseFOV = 103f;
    public float unityFOV = 90f;
    public Transform playerCamera;

    private CharacterController controller;
    private float xRotation = 0f;
    private float mouseSensitivity;
    private Vector3 velocity;
    private bool isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        if (controller == null)
        {
            Debug.LogError("CharacterController is missing on Player!");
        }
        Cursor.lockState = CursorLockMode.Locked;
        mouseSensitivity = valorantSens * (360f / baseFOV) * (unityFOV / baseFOV);
    }

    void Update()
    {
        // ✅ Ground Check
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Reset downward force
        }

        // ✅ Sprinting (Hold Shift)
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;

        // ✅ Movement Input
        float x = Input.GetAxis("Horizontal"); // A, D
        float z = Input.GetAxis("Vertical");   // W, S
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * currentSpeed * Time.deltaTime);

        // ✅ Jumping (Press Space)
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // ✅ Apply Gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // ✅ Mouse Look
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
}
