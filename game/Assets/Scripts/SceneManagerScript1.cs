using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript1 : MonoBehaviour
{
    public string sceneName;
    public string defaultGravity = "Up";
    public float timer = 2f;
    public GameObject t1;
    public GameObject t2;

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
            t1.SetActive(false);
            t2.SetActive(true);
            GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>().isKinematic = false;
            if(timer < 0 && Input.GetButtonDown("Fire1")){
                SceneManager.LoadScene(sceneName);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        
    }
}
