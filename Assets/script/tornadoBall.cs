using UnityEngine;

public class tornadoBall : MonoBehaviour
{
    public ParticleSystem tornado;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(tornado, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
