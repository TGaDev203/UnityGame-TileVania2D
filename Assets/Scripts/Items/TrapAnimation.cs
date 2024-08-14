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

    //! On Trigger Enter
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            trapAnimation.SetBool("isTakenDamage", true);
            trapAnimation.SetBool("isReversed", false);
        }
    }

    //! On Trigger Exit
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            trapAnimation.SetBool("isReversed", true);
            trapAnimation.SetTrigger("takeDamage");
        }
    }
}