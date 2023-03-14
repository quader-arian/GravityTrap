using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    public string sceneName;
    void Update(){
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("EnemyComplete");
        if(enemies.Length <= 0){
            SceneManager.LoadScene(sceneName);
        }
    }
}
