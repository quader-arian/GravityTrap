using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ImpactDestroy : MonoBehaviour
{
    public float timer;
    private CameraShake shake;

    void OnTriggerEnter2D(Collider2D col){
        if (col.gameObject.tag == "Player"){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        StartCoroutine(shake.Shake(.05f, .05f));
        Destroy(gameObject);
    }

    void Start(){
        Destroy(gameObject, timer);
        shake = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>();
    }
}
