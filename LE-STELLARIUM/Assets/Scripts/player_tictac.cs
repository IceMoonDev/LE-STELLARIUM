using UnityEngine;
using UnityEngine.InputSystem;

public class player_tictac : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 500.0f;

    // Drag and drop your Input Actions into these slots in the Unity Inspector!
    [SerializeField] private InputActionReference moveActionToUse;
    [SerializeField] private InputActionReference jumpActionToUse;

    private Rigidbody2D local_rigidbody = null;
    private Vector2 movement;
    private bool isGrounded = true;

    // Enable the actions when the object is active
    private void OnEnable()
    {
        if (moveActionToUse != null) moveActionToUse.action.Enable();
        if (jumpActionToUse != null) jumpActionToUse.action.Enable();
    }

    // Disable the actions when the object is destroyed/disabled to prevent memory leaks
    private void OnDisable()
    {
        if (moveActionToUse != null) moveActionToUse.action.Disable();
        if (jumpActionToUse != null) jumpActionToUse.action.Disable();
    }

    void Start()
    {
        local_rigidbody = GetComponent<Rigidbody2D>();
        if (local_rigidbody == null)
        {
            Debug.LogError("Rigidbody2D component not found on this GameObject.");
        }
    }

    void Update()
    {
        // Read values from the InputActionReference
        movement.x = moveActionToUse.action.ReadValue<Vector2>().x;

        if (local_rigidbody.linearVelocity.y > 0)
        {
            movement.y = 0;
        }

        if (local_rigidbody != null)
        {
            local_rigidbody.linearVelocity = new Vector2(movement.x * moveSpeed, local_rigidbody.linearVelocity.y);
        }

        // Check if jump was pressed AND if the ball is on the ground
        if (jumpActionToUse.action.WasPressedThisFrame() && isGrounded)
        {
            local_rigidbody.AddForce(Vector2.up * 500.0f);
            isGrounded = false;
            Debug.Log("jump");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            Debug.Log("ground");
        }
    }
}
