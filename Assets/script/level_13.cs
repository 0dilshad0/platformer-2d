using UnityEngine;

public class level_13 : MonoBehaviour
{
    public Animator animator;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        animator.SetTrigger("play");
    }
}
