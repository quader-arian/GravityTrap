using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyScript : MonoBehaviour
{
    private CameraShake shake;
    [Header("FX")]
    public GameObject blueFX;
    public GameObject greenFX;
    public GameObject redFX;

    [Header("Move Settings")]
    public float moveSpeed;
    public Transform[] movePoints;
    public int currentMovePoint = 0;

    [Header("Shoot Settings")]
    public bool shoot;
    public float shootInterval;
    public float shootSpeed;
    private float timer;
    public GameObject bullet;
    public Transform firePoint;

    [Header("Kill and Death Settings")]
    public bool onContactKill;
    public bool onContactExplode;
    public bool onAreaContactKill;
    public bool onAreaContactExplode;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "AttackArea"){
            if(onAreaContactKill){
                Destroy(gameObject, 0.5f);
                Instantiate(redFX, transform.position, Quaternion.identity);
            }
            if(onAreaContactExplode){
                Instantiate(redFX, transform.position, Quaternion.identity);
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "AttackArea"){
            if(onAreaContactKill){
                Instantiate(redFX, transform.position, Quaternion.identity);
                Destroy(gameObject, 0.5f);
            }
            if(onAreaContactExplode){
                Instantiate(redFX, transform.position, Quaternion.identity);
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player"){
            if(onContactKill){
                Instantiate(blueFX, transform.position, Quaternion.identity);
                Destroy(gameObject, 0.05f);
                StartCoroutine(shake.Shake(.1f, .1f));
            }
            if(onContactExplode){
                Instantiate(redFX, transform.position, Quaternion.identity);
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player"){
            if(onContactKill){
                Instantiate(blueFX, transform.position, Quaternion.identity);
                Destroy(gameObject, 0.05f);
                StartCoroutine(shake.Shake(.1f, .1f));
            }
            if(onContactExplode){
                Instantiate(redFX, transform.position, Quaternion.identity);
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    void Start(){
        timer = shootInterval;
        shake = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>();
    }

    void Update(){
        if(moveSpeed>0){
            if(transform.position == movePoints[currentMovePoint].position){
                currentMovePoint++;
                if(currentMovePoint >= movePoints.Length){
                    currentMovePoint = 0;
                }
            }
            float step = moveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, movePoints[currentMovePoint].position, step);
        }

        if(shoot){
            if(timer < 0){
                Instantiate(greenFX, transform.position, Quaternion.identity);
                GameObject currBullet = Instantiate(bullet, firePoint.position, firePoint.rotation);
                Rigidbody2D rbody = currBullet.GetComponent<Rigidbody2D>();

                rbody.AddForce(firePoint.up * shootSpeed, ForceMode2D.Impulse);
                timer = shootInterval;
            }
            timer = timer - Time.deltaTime;
        }
    }
}
