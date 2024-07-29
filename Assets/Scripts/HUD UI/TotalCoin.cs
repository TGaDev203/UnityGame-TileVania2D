using TMPro;
using UnityEngine;

public class TotalCoin : MonoBehaviour
{
    //! Component Variables
    [SerializeField] private TextMeshProUGUI coinText;

    private int totalCoinCollected = 0;

    //! Lifecycle Methods
    private void Awake()
    {
        coinText = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        UpdateCoinText();
    }

    //! Counting Coin
    public void CountCoin(int amount)
    {
        totalCoinCollected += amount;
        UpdateCoinText();
    }

    //! Updating Coin Text
    private void UpdateCoinText()
    {
        if (coinText != null)
        {
            coinText.text = "Total: " + totalCoinCollected.ToString();
        }
    }
}
