using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance {get; private set;}

    [SerializeField] private AudioClip hitEnemySound;
    [SerializeField] private AudioClip coinEnemySound;

    [SerializeField] private float loadDelay;

    private AudioSource audioSource;

    private void Awake()
    {
        InitializeComponents();
    }

    //! Initialization
    private void InitializeComponents()
    {
        audioSource = GetComponent<AudioSource>();
        Instance = this;
    }

    public void PlayerHitSound()
    {
        audioSource.PlayOneShot(hitEnemySound);
    }

    public void PlayCoinSound()
    {
        audioSource.PlayOneShot(coinEnemySound);
    }
}
