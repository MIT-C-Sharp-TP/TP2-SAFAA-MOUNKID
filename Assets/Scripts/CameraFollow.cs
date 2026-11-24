
using UnityEngine;

// Keeps the camera behind the car.
// LateUpdate runs after the car has moved.
public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset = new Vector3(0f, 2.2f, -4.5f);
    [SerializeField] private float smoothing = 5f;

    void LateUpdate()
    {
        if (target == null)
        {
            return;
        }

        // The offset turns with the car.
        Vector3 wanted = target.position + target.TransformDirection(offset);

        // Smooth camera movement.
        transform.position = Vector3.Lerp(
            transform.position,
            wanted,
            smoothing * Time.deltaTime
        );

        // Camera looks at the car.
        transform.LookAt(target.position + Vector3.up * 0.3f);
    }
}
