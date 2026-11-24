
using UnityEngine;

public class WheelRotation : MonoBehaviour
{
    [SerializeField] private Rigidbody carRigidbody;
    [SerializeField] private float rotationSpeed = 360f;

    void Update()
    {
        if (carRigidbody == null)
            return;

        if (carRigidbody.linearVelocity.magnitude > 0.1f)
        {
            transform.Rotate(
                Vector3.right,
                rotationSpeed * Time.deltaTime,
                Space.Self
            );
        }
    }
}
