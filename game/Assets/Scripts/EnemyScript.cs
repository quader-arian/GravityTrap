using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyScript : MonoBehaviour
{
    public CameraShake shake;
    
    public bool damage;
    public int damageAmount;

    public bool move;
    public int moveSpeed;

    public bool onContactKill;
    public bool onContactExplode;
    public bool onAreaContactKill;
    public bool onAreaContactExplode;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "AttackArea"){
            if(onAreaContactKill){
                Destroy(gameObject, 0.5f);
            }
            if(onAreaContactExplode){
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "AttackArea"){
            if(onAreaContactKill){
                Destroy(gameObject, 0.5f);
            }
            if(onAreaContactExplode){
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player"){
            if(onContactKill){
                Destroy(gameObject, 0.05f);
                StartCoroutine(shake.Shake(.1f, .1f));
            }
            if(onContactExplode){
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player"){
            if(onContactKill){
                Destroy(gameObject, 0.05f);
                StartCoroutine(shake.Shake(.1f, .1f));
            }
            if(onContactExplode){
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}
