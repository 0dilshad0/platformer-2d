using UnityEngine;

public class one : MonoBehaviour
{
    public Animator animator;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        animator.SetBool("up", true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        animator.SetBool("up", false);
    }
}
