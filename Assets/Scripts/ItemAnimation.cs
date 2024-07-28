using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAnimation : MonoBehaviour
{
    private Animator coinAnimation;

    [SerializeField] float destroyDelay = 0.1f;

    void Awake()
    {
        coinAnimation = GetComponent<Animator>();
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CoinFlipAnimation();
            Invoke("DestroyCoin", destroyDelay);
        }
    }

    private void CoinFlipAnimation()
    {
        if (gameObject.CompareTag("Coin"))
        {
            coinAnimation.SetBool("isCollected", true);
        }
    }

    private void DestroyCoin()
    {
            Destroy(gameObject);
    }
}
