using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ImpactDestroy : MonoBehaviour
{
    public float timer;
    public GameObject redFX;
    private CameraController cameraController;

    void OnTriggerEnter2D(Collider2D col){
        if (col.gameObject.tag == "Player"){
            Instantiate(redFX, transform.position, Quaternion.identity);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }else if(col.gameObject.tag == "EnemyComplete"){
            Instantiate(redFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }else if(col.gameObject.tag == "Up" || col.gameObject.tag == "Right" || col.gameObject.tag == "Left" || col.gameObject.tag == "Down"){
            Instantiate(redFX, transform.position, Quaternion.identity);
            cameraController.StartShake(0.05f, 0.05f);
            Destroy(gameObject);
        }
    }

    void Start(){
        Destroy(gameObject, timer);
        cameraController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
    }
}
