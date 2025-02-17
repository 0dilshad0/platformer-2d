using UnityEngine;

public class two : MonoBehaviour
{
    public Animator oneAnimator;
    public Animator twoAnimator;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("boxOne") || collision.gameObject.CompareTag("boxTwo"))
        {
            oneAnimator.SetBool("up", true);
        }
        if ( collision.gameObject.CompareTag("boxTwo"))
        {
            twoAnimator.SetBool("up", true);
        }

    }
  
}
