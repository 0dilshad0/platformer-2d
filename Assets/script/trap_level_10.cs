using UnityEngine;

public class trap_level_10 : MonoBehaviour
{
    public ParticleSystem ParticleSystem;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Ground"))
        {
            Instantiate(ParticleSystem, collision.gameObject.transform.position,Quaternion.identity);
            Destroy(collision.gameObject);
        }
    }
   
}
