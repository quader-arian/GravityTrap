using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ImpactDestroy : MonoBehaviour
{
    public float timer;
    public GameObject redFX;
    private CameraShake shake;

    void OnTriggerEnter2D(Collider2D col){
        if (col.gameObject.tag == "Player"){
            Instantiate(redFX, transform.position, Quaternion.identity);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        Instantiate(redFX, transform.position, Quaternion.identity);
        StartCoroutine(shake.Shake(.05f, .05f));
        Destroy(gameObject);
    }

    void Start(){
        Destroy(gameObject, timer);
        shake = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>();
    }
}
