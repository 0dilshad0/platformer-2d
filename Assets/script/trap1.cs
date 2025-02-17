using System.Collections;
using UnityEngine;

public class trap1 : MonoBehaviour
{
    private Rigidbody2D rb;
    public float force = 50;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(swing());
        
    }

   
    void Update()
    {
        rb.AddForceX(force);
    }
   IEnumerator swing()
    {
        while(force!=0)
        {
            yield return new WaitForSecondsRealtime(2f);

            force = force * -1;
        }
        
    }
}
