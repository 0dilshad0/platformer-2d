using UnityEngine;

public class trap_level_3 : MonoBehaviour
{
    public Animator bridge;
    public GameObject Light;
    

    void Start()
    {
        
    }

    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        bridge.SetBool("open",true);
        Light.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        bridge.SetBool("open", false);
        Light.SetActive(false);
    }
}
