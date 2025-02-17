using UnityEngine;

public class trigger_10 : MonoBehaviour
{
    public Rigidbody2D rb;
    void Start()
    {
        rb.bodyType = RigidbodyType2D.Static;
    }

  
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
    }
}
