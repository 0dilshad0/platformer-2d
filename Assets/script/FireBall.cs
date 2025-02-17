using UnityEngine;

public class FireBall : MonoBehaviour
{
    public ParticleSystem fire;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(fire, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
