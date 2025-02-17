using UnityEngine;

public class spike : MonoBehaviour
{
    public ParticleSystem explotion;
    public AudioSource explotionSound;

    private void Awake()
    {
        explotion.Pause();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(explotion, transform.position, Quaternion.identity);
        Destroy(gameObject);
        explotionSound.Play();

    }
}
