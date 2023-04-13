using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void StartGame()
    {
        Time.timeScale = 1;
        PauseScript.gameIsPaused = false;
        SceneManager.LoadScene("Level1");
    }
    public void ReturnMain()
    {
        Time.timeScale = 1;
        PauseScript.gameIsPaused = false;
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}