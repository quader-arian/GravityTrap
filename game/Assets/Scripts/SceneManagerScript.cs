using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public string sceneName;
    public string defaultGravity = "Up";
    public GameObject signal;

    void Start(){
        if (defaultGravity == "Up"){
            Physics2D.gravity = new Vector2(0, 9.8f);
            signal.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if(defaultGravity == "Left"){
            Physics2D.gravity = new Vector2(-9.8f, 0);
            signal.transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        if(defaultGravity == "Right"){
            Physics2D.gravity = new Vector2(9.8f, 0);
            signal.transform.rotation = Quaternion.Euler(0, 0, 270);
        }
        if(defaultGravity == "Down"){
            Physics2D.gravity = new Vector2(0, -9.8f);
            signal.transform.rotation = Quaternion.Euler(0, 0, 180);
        }
    }

    void Update(){
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("EnemyComplete");
        if(enemies.Length <= 0){
            SceneManager.LoadScene(sceneName);
        }
    }
}
