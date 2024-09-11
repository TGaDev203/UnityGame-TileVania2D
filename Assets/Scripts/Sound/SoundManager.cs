using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [SerializeField] private AudioClip hitEnemySound;
    [SerializeField] private AudioClip coinEnemySound;
    [SerializeField] private AudioClip waterSplashSound;
    [SerializeField] private AudioClip waterWalkingSound;
    [SerializeField] private AudioClip menuButtonProgressSound;
    [SerializeField] private AudioClip menuButtonEndSound;

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

    public void PlayWaterSplashSound()
    {
        audioSource.PlayOneShot(waterSplashSound);
    }

    public void PlayWaterWalkingSound()
    {
        audioSource.clip = waterWalkingSound;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void StopWaterWalkingSound()
    {
        if (audioSource != null)
        {
            audioSource.loop = false;
            audioSource.Stop();
        }
    }

    public void PlayMenuButtonProgressSound()
    {
        audioSource.PlayOneShot(menuButtonProgressSound);
    }

    public void PlayMenuButtonEndSound()
    {
        audioSource.PlayOneShot(menuButtonEndSound);
    }
}
