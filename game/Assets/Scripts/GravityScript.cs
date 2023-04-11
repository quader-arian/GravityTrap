using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityScript : MonoBehaviour
{
    private CameraController cameraController;
    public GameObject signal;
    public bool onGround = false;
    public bool onGroundExit = false;

    public GameObject idle;
    public GameObject run;
    public GameObject air;

    void Start()
    {
        cameraController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        GravityChange(col);
        cameraController.StartShake(0.1f, 0.1f);
        onGroundExit = false;
    }
    void OnCollisionStay2D(Collision2D col)
    {
        GravityChange(col);
        if((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) && !run.activeSelf){
            idle.SetActive(false);
            run.SetActive(true);
            air.SetActive(false);
        }
        onGroundExit = false;
    }
    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Up" || col.gameObject.tag == "Left" || col.gameObject.tag == "Right" || col.gameObject.tag == "Down")
        {
            onGround = false;
            onGroundExit = true;
            idle.SetActive(false);
            run.SetActive(false);
            air.SetActive(true);
        }
        cameraController.StartShake(0.1f, 0.1f);
    }

    void GravityChange(Collision2D col){
        if (col.gameObject.tag == "Up"){
            Physics2D.gravity = new Vector2(0, 9.8f);
            signal.transform.rotation = Quaternion.Euler(0, 0, 0);
            transform.rotation = Quaternion.Euler(0, 0, 180);
            if(GetComponent<Rigidbody2D>().velocity == Vector2.zero){
                idle.SetActive(true);
                run.SetActive(false);
                air.SetActive(false);
            }else{
                idle.SetActive(false);
                run.SetActive(true);
                air.SetActive(false);
            }
            onGround = true;
        }
        if(col.gameObject.tag == "Left"){
            Physics2D.gravity = new Vector2(-9.8f, 0);
            signal.transform.rotation = Quaternion.Euler(0, 0, 90);
            transform.rotation = Quaternion.Euler(0, 0, 270);
            if(GetComponent<Rigidbody2D>().velocity == Vector2.zero){
                idle.SetActive(true);
                run.SetActive(false);
                air.SetActive(false);
            }else{
                idle.SetActive(false);
                run.SetActive(true);
                air.SetActive(false);
            }
            onGround = true;
        }
        if(col.gameObject.tag == "Right"){
            Physics2D.gravity = new Vector2(9.8f, 0);
            signal.transform.rotation = Quaternion.Euler(0, 0, 270);
            transform.rotation = Quaternion.Euler(0, 0, 90);
            if(GetComponent<Rigidbody2D>().velocity == Vector2.zero){
                idle.SetActive(true);
                run.SetActive(false);
                air.SetActive(false);
            }else{
                idle.SetActive(false);
                run.SetActive(true);
                air.SetActive(false);
            }
            onGround = true;
        }
        if(col.gameObject.tag == "Down"){
            Physics2D.gravity = new Vector2(0, -9.8f);
            signal.transform.rotation = Quaternion.Euler(0, 0, 180);
            transform.rotation = Quaternion.Euler(0, 0, 0);
            if(GetComponent<Rigidbody2D>().velocity == Vector2.zero){
                idle.SetActive(true);
                run.SetActive(false);
                air.SetActive(false);
            }else{
                idle.SetActive(false);
                run.SetActive(true);
                air.SetActive(false);
            }
            onGround = true;
        }
    }
}
