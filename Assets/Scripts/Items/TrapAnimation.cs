using UnityEngine;

public class TrapAnimation : MonoBehaviour
{
    //! Components
    private Animator trapAnimation;

    private void Awake()
    {
        InitializeComponents();
    }

    private void InitializeComponents()
    {
        trapAnimation = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            trapAnimation.SetBool("isTakenDamage", true);
        }
        else
        {
            trapAnimation.SetBool("isTakenDamage", false);
        }
    }

    //! Method To Handle Trap Animation
    // private void HandleTrapAnimation()
    // {
    //     trapAnimation.SetTrigger("TakenDamage");
    // }
}
