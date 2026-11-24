
using System;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public static event Action<int> Collected;

    [SerializeField] private float rotationSpeed = 90f;
    [SerializeField] private int points = 10;

    private bool alreadyCollected = false;

    void Update()
    {
        transform.Rotate(
            0f,
            0f,
            rotationSpeed * Time.deltaTime,
            Space.Self
        );
    }

    void OnTriggerEnter(Collider other)
    {
        if (alreadyCollected)
            return;

        if (!other.CompareTag("Player"))
            return;

        alreadyCollected = true;

        Debug.Log("EVENEMENT DECLENCHE PAR : " + gameObject.name);

        Collected?.Invoke(points);

        Destroy(gameObject);
    }

    [RuntimeInitializeOnLoadMethod(
        RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void ResetCollectedEvent()
    {
        Collected = null;
    }
}
