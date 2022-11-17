using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
        frozeGame();
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        unfrozeGame();
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }

    private void frozeGame()
    {
        Time.timeScale = 0;
    }
    
    private void unfrozeGame()
    {
        Time.timeScale = 1;
    }
}
