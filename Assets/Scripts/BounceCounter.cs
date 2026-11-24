using UnityEngine;

// Counts how many times the ball hits something (Lab, step 11).
[RequireComponent(typeof(Rigidbody))]
public class BounceCounter : MonoBehaviour
{
    private int bounces;

    void OnCollisionEnter(Collision collision)
    {
        bounces++;
        string other = collision.gameObject.name;
        float impactSpeed = collision.relativeVelocity.magnitude;
        Debug.Log($"Bounce {bounces} on {other}, {impactSpeed:0.0} m/s");
    }
}