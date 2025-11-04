using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Move")]
    public float walkSpeed = 6f;
    public float runSpeed  = 12f;

    [Header("Jump/Gravity")]
    public float jumpHeight = 2.0f;   // how high the jump goes
    public float gravity    = 20f;    // pull-down strength

    private CharacterController controller;
    private Vector3 move;             // x/z input (world-forward/right)
    private float verticalVel;        // y-velocity we integrate with gravity

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible   = false;
    }

    void Update()
    {
        // --- Horizontal movement (XZ) ---
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // Move relative to world axes (replace with camera forward/right if needed)
        Vector3 forward = Vector3.forward;
        Vector3 right   = Vector3.right;

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float speed    = isRunning ? runSpeed : walkSpeed;

        move = (forward * v + right * h);
        if (move.sqrMagnitude > 1f) move.Normalize();
        move *= speed;

        // --- Grounded & Jump ---
        if (controller.isGrounded)
        {
            // keep a tiny downward push so we stay grounded on slopes
            if (verticalVel < -2f) verticalVel = -2f;

            // Space to jump (same behavior as your reference)
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // v = sqrt(2gh): compute initial upward velocity for desired jumpHeight
                verticalVel = Mathf.Sqrt(2f * gravity * jumpHeight);
            }
        }
        else
        {
            // Apply gravity while in air
            verticalVel -= gravity * Time.deltaTime;
        }

        // --- Apply motion ---
        Vector3 velocity = new Vector3(move.x, verticalVel, move.z);
        controller.Move(velocity * Time.deltaTime);

        // Face movement direction (optional)
        Vector3 look = new Vector3(move.x, 0f, move.z);
        if (look.sqrMagnitude > 0.001f)
            transform.rotation = Quaternion.LookRotation(look, Vector3.up);
    }
}
