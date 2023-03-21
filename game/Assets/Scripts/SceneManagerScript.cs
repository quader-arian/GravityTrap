using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public string sceneName;
    public float defaultGravity = 9.8f;
    void Update(){
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("EnemyComplete");
        if(enemies.Length <= 0){
            SceneManager.LoadScene(sceneName);
        }
    }
}
