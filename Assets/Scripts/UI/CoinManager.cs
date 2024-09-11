using TMPro;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    //! Component
    [SerializeField] private TextMeshProUGUI coinText;
    private int totalCoinCollected = 0;

    private void Awake()
    {
        InitializeComponents();
    }

    //! Initialization
    private void InitializeComponents()
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
