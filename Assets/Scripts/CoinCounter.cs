
using UnityEngine;
using TMPro;

// Counts the collected coins and updates the score.
// Listens to the Coin.Collected event.

public class CoinCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text coinsText;

    private int coins;
    private int coinsToWin;
    private int score = 0;

    void OnEnable()
    {
        // Subscribe to the coin collection event.
        Coin.Collected += AddCoin;
    }

    void OnDisable()
    {
        // Unsubscribe when this object is disabled.
        Coin.Collected -= AddCoin;
    }

    void Start()
    {
        // Count the coins present at the beginning.
        coinsToWin = FindObjectsByType<Coin>(
            FindObjectsSortMode.None
        ).Length;

        Debug.Log($"Collect the {coinsToWin} coins!");

        UpdateUI();
    }

    private void AddCoin(int points)
    {
        coins++;

        // Add the points received from the collected coin.
        score += points;

        Debug.Log($"Coins: {coins}/{coinsToWin}");
        Debug.Log($"Score: {score}");

        UpdateUI();

        if (coins == coinsToWin)
        {
            Debug.Log("All coins collected!");
        }
    }

    private void UpdateUI()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {score}";
        }

        if (coinsText != null)
        {
            coinsText.text = $"Coins: {coins}/{coinsToWin}";
        }
    }
}
