using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public string sceneName;
    public string defaultGravity = "Up";
    public float timer = 2f;
    public GameObject scared;
    public GameObject happy;

    void Start(){
        if (defaultGravity == "Up"){
            Physics2D.gravity = new Vector2(0, 9.8f);
        }
        if(defaultGravity == "Left"){
            Physics2D.gravity = new Vector2(-9.8f, 0);
        }
        if(defaultGravity == "Right"){
            Physics2D.gravity = new Vector2(9.8f, 0);
        }
        if(defaultGravity == "Down"){
            Physics2D.gravity = new Vector2(0, -9.8f);
        }
    }

    void Update(){
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("EnemyComplete");
        if(enemies.Length <= 0){
            timer -= Time.deltaTime;
            GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>().isKinematic = false;
            happy.SetActive(true);
            scared.SetActive(false);
            if(timer < 0){
                SceneManager.LoadScene(sceneName);
            }
        }
    }
}
