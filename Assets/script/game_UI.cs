using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class game_UI : MonoBehaviour
{
    public int currentLevel;
    public GameObject over;
    public GameObject win;
    public GameObject pause;

    public sword_man sword_Man;
 
    void Start()
    {

      
        over.SetActive(false);
        win.SetActive(false);
        pause.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    
    void Update()
    {
        if(sword_Man.currentHealth<=0)
        {
            gameOver();
        }
        if(Input.GetKey(KeyCode.Escape))
        {
            paused();
           
        }
        if(sword_Man.win)
        {
            gameWin();
        }
    }
    private void paused()
    {
        pause.SetActive(true);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
    }
    private void gameOver()
    {
        over.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }
    private void gameWin()
    {
        win.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }
    
    public void back()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
    public void resuse()
    {
        pause.SetActive(false);
        Time.timeScale =1f;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(currentLevel);
    }
    public void Next()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(currentLevel+1);
    }

}
