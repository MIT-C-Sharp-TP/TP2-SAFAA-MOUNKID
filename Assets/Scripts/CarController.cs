
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class CarController : MonoBehaviour
{
    [Header("Driving")]
    [SerializeField] private float acceleration = 6f;
    [SerializeField] private float maxSpeed = 8f;
    [SerializeField] private float brakeForce = 14f;
    [SerializeField] private float drag = 1.5f;
    [SerializeField] private float turnSpeed = 80f;

    [Header("Jump")]
    [SerializeField] private float jumpSpeed = 4.5f;

    private Rigidbody rb;
    private Collider carCollider;

    private float throttle;
    private float steering;
    private bool braking;
    private bool jumpWanted;
    private float speed;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        carCollider = GetComponent<Collider>();
    }

    void Update()
    {
        Keyboard keyboard = Keyboard.current;

        if (keyboard == null)
            return;

        throttle =
            (keyboard.wKey.isPressed ? 1f : 0f)
            - (keyboard.sKey.isPressed ? 1f : 0f);

        steering =
            (keyboard.dKey.isPressed ? 1f : 0f)
            - (keyboard.aKey.isPressed ? 1f : 0f);

        braking = keyboard.leftShiftKey.isPressed;

        if (keyboard.spaceKey.wasPressedThisFrame)
        {
            jumpWanted = true;
        }
    }

    void FixedUpdate()
    {
        float step = Time.fixedDeltaTime;

        // Acceleration and braking
        if (braking)
        {
            speed = Mathf.MoveTowards(
                speed, 0f, brakeForce * step
            );
        }
        else if (throttle != 0f)
        {
            speed += throttle * acceleration * step;
        }
        else
        {
            speed = Mathf.MoveTowards(
                speed, 0f, drag * step
            );
        }

        speed = Mathf.Clamp(
            speed, -maxSpeed * 0.4f, maxSpeed
        );

        // Car movement
        Vector3 velocity = transform.forward * speed;
        velocity.y = rb.linearVelocity.y;
        rb.linearVelocity = velocity;

        // Steering
        if (Mathf.Abs(speed) > 0.3f)
        {
            float angle =
                steering * Mathf.Sign(speed) * turnSpeed * step;

            rb.MoveRotation(
                rb.rotation * Quaternion.Euler(0f, angle, 0f)
            );
        }

        // Jump
        if (jumpWanted)
        {
            jumpWanted = false;

            if (Grounded())
            {
                Vector3 jumpVelocity = rb.linearVelocity;
                jumpVelocity.y = jumpSpeed;
                rb.linearVelocity = jumpVelocity;
            }
        }
    }

    private bool Grounded()
    {
        if (carCollider == null)
            return false;

        Bounds bounds = carCollider.bounds;

        Vector3 origin = bounds.center + Vector3.up * 0.05f;
        float distance = bounds.extents.y + 0.25f;

        RaycastHit[] hits = Physics.RaycastAll(
            origin,
            Vector3.down,
            distance
        );

        foreach (RaycastHit hit in hits)
        {
            // Ignore the car and its own colliders.
            if (hit.collider.attachedRigidbody == rb)
                continue;

            return true;
        }

        return false;
    }
}
