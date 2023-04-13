using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject menu;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameIsPaused = !gameIsPaused;
            if(gameIsPaused){
                menu.SetActive(true);
                Time.timeScale = 0;
            }else{
                menu.SetActive(false);
                Time.timeScale = 1;
            }
        }
    }

    public void Resume(){
        menu.SetActive(false);
        Time.timeScale = 1;
        gameIsPaused = false;
    }
}
