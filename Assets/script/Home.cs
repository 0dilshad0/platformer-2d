using UnityEngine;
using UnityEngine.SceneManagement;

public class Home : MonoBehaviour
{
    public GameObject level;  
    void Start()
    {
        level.SetActive(false);
    }

    void Update()
    {
        
    }

    public void play()
    {
        level.SetActive(true);
    }
    public  void exit()
    {
        Application.Quit();
    }
    public void back()
    {
        level.SetActive(false);
    }
    public void levels(int current)
    {
        SceneManager.LoadScene(current);
    }
}
