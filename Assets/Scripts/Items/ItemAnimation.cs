using UnityEngine;

public class ItemAnimation : MonoBehaviour
{
    //! Component Variables
    private Animator coinAnimation;

    [SerializeField] private float destroyDelay;

    private bool hasBeenPicked = false;

    //! Lifecycle Methods
    void Awake()
    {
        coinAnimation = GetComponent<Animator>();
    }

    //! On Trigger Enter
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasBeenPicked && other.CompareTag("Player"))
        {
            hasBeenPicked = true;

            int coinValue = 1;

            TotalCoin totalCoinCollected = FindObjectOfType<TotalCoin>();

            if (totalCoinCollected != null)
            {
                totalCoinCollected.CountCoin(coinValue);
            }

            CoinFlipAnimation();

            GetComponent<Collider2D>().enabled = false; // Turn off collider to avoid recollecting coin

            Invoke("DestroyCoin", destroyDelay);
        }
    }

    //! Coin Flip Animation
    private void CoinFlipAnimation()
    {
        if (gameObject.CompareTag("Coin"))
        {
            coinAnimation.SetBool("isCollected", true);
        }
    }

    //! Destroying Coin
    private void DestroyCoin()
    {
        Destroy(gameObject);
    }
}
