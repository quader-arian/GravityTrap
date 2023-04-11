using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyScript : MonoBehaviour
{
    private CameraController cameraController;
    [Header("FX")]
    public GameObject blueFX;
    public GameObject greenFX;
    public GameObject redFX;

    [Header("Move Settings")]
    public float moveSpeed;
    public Transform[] movePoints;
    public int currentMovePoint = 0;
    private Vector3[] stayPoints = new Vector3[2];

    [Header("Shoot Settings")]
    public bool shoot;
    public float shootInterval;
    public float shootSpeed;
    private float timer;
    public GameObject bullet;
    public Transform firePoint;

    [Header("Kill and Death Settings")]
    public bool dying = false;
    public bool restarting = false;
    public float doneTimer = 1f;
    public bool onContactKill;
    public bool onContactExplode;
    public bool onAreaContactKill;
    public bool onAreaContactExplode;

    [Header("Animations")]
    public GameObject [] animationOff;
    public GameObject animationOn;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "AttackArea"){
            if(onAreaContactKill){
                Instantiate(redFX, transform.position, Quaternion.identity);
                dying = true;
            }
            if(onAreaContactExplode){
                Instantiate(redFX, transform.position, Quaternion.identity);
                restarting = true;
            }
        }
        if (col.gameObject.tag == "Player"){
            if(onContactKill){
                Instantiate(blueFX, transform.position, Quaternion.identity);
                dying = true;
                cameraController.StartShake(0.1f, 0.1f);
            }
            if(onContactExplode){
                Instantiate(redFX, transform.position, Quaternion.identity);
                restarting = true;
            }
        }
        if (col.gameObject.tag == "Bullet"){
            if(!onAreaContactExplode){
                Instantiate(blueFX, transform.position, Quaternion.identity);
                dying = true;
                cameraController.StartShake(0.1f, 0.1f);
            }
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "AttackArea"){
            if(onAreaContactKill){
                Instantiate(redFX, transform.position, Quaternion.identity);
                dying = true;
            }
            if(onAreaContactExplode){
                Instantiate(greenFX, transform.position, Quaternion.identity);
                restarting = true;
            }
        }
        if (col.gameObject.tag == "Player"){
            if(onContactKill){
                Instantiate(blueFX, transform.position, Quaternion.identity);
                dying = true;
                cameraController.StartShake(0.1f, 0.1f);
            }
            if(onContactExplode){
                Instantiate(redFX, transform.position, Quaternion.identity);
                restarting = true;
            }
        }
        if (col.gameObject.tag == "Bullet"){
            if(!onAreaContactExplode){
                Instantiate(blueFX, transform.position, Quaternion.identity);
                dying = true;
                cameraController.StartShake(0.1f, 0.1f);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player"){
            if(onContactKill){
                Instantiate(blueFX, transform.position, Quaternion.identity);
                dying = true;
                cameraController.StartShake(0.1f, 0.1f);
            }
            if(onContactExplode){
                Instantiate(redFX, transform.position, Quaternion.identity);
                restarting = true;
            }
        }if (col.gameObject.tag == "Bullet"){
            if(!onAreaContactExplode){
                Instantiate(blueFX, transform.position, Quaternion.identity);
                dying = true;
                restarting = true;
            }
        }
    }
    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player"){
            if(onContactKill){
                Instantiate(blueFX, transform.position, Quaternion.identity);
                dying = true;
                cameraController.StartShake(0.1f, 0.1f);
            }
            if(onContactExplode){
                Instantiate(redFX, transform.position, Quaternion.identity);
                restarting = true;
            }
        }if (col.gameObject.tag == "Bullet"){
            if(!onAreaContactExplode){
                Instantiate(blueFX, transform.position, Quaternion.identity);
                dying = true;
                cameraController.StartShake(0.1f, 0.1f);
            }
        }
    }

    void Start(){
        timer = shootInterval;
        cameraController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
        stayPoints[0] = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);
        stayPoints[1] = new Vector3(transform.position.x, transform.position.y - 0.1f, transform.position.z);
    }

    void Update(){
        if(dying || restarting){
            if(animationOn != null){
                foreach(GameObject o in animationOff){
                    o.SetActive(false);
                }
                animationOn.SetActive(true);
            }
            
            if(GetComponent<Rigidbody2D>() != null){
                Destroy(GetComponent<Rigidbody2D>());
                //Destroy(GetComponent<BoxCollider2D>());
            }

            doneTimer -= Time.deltaTime;
            if(doneTimer < 0){
                if(dying && onContactKill){
                    Destroy(gameObject);
                }else if(dying){
                    Destroy(gameObject);
                }
                
                if(restarting){
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            }

        }else if(moveSpeed>0){
            if(transform.position == movePoints[currentMovePoint].position){
                currentMovePoint++;
                if(currentMovePoint >= movePoints.Length){
                    currentMovePoint = 0;
                }
            }
            float step = moveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, movePoints[currentMovePoint].position, step);
        }else{
            if(transform.position == stayPoints[currentMovePoint]){
                currentMovePoint++;
                if(currentMovePoint >= stayPoints.Length){
                    currentMovePoint = 0;
                }
            }
            float step = 0.1f * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, stayPoints[currentMovePoint], step);
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
