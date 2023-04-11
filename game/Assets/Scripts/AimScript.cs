using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimScript : MonoBehaviour
{
    private Camera cam;
    private Vector3 mousePos;
    private Vector3 playerPos;
    private float s;

    public GameObject[] anim = new GameObject[2];
    
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        s = anim[0].transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        playerPos = cam.ScreenToWorldPoint(GameObject.FindGameObjectWithTag("Player").transform.position);
        Vector3 lookDir = mousePos - transform.position;

        float rotZ = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);

        if(Physics2D.gravity == new Vector2(0, 9.8f) && GameObject.FindWithTag("Player").GetComponent<GravityScript>().onGround){
            if(lookDir.x > 0){
                anim[0].transform.localScale = new Vector3(-s, s, s);
                anim[1].transform.localScale = new Vector3(-s, s, s);
            }else{
                anim[0].transform.localScale = new Vector3(s, s, s);
                anim[1].transform.localScale = new Vector3(s, s, s);
            }
        }else if(Physics2D.gravity == new Vector2(0, -9.8f) && GameObject.FindWithTag("Player").GetComponent<GravityScript>().onGround){
            if(lookDir.x > 0){
                anim[0].transform.localScale = new Vector3(s, s, s);
                anim[1].transform.localScale = new Vector3(s, s, s);
            }else{
                anim[0].transform.localScale = new Vector3(-s, s, s);
                anim[1].transform.localScale = new Vector3(-s, s, s);
            }
        }else if(Physics2D.gravity == new Vector2(9.8f, 0) && GameObject.FindWithTag("Player").GetComponent<GravityScript>().onGround){
            if(lookDir.y > 0){
                anim[0].transform.localScale = new Vector3(s, s, s);
                anim[1].transform.localScale = new Vector3(s, s, s);
            }else{
                anim[0].transform.localScale = new Vector3(-s, s, s);
                anim[1].transform.localScale = new Vector3(-s, s, s);
            }
        }else if(Physics2D.gravity == new Vector2(-9.8f, 0) && GameObject.FindWithTag("Player").GetComponent<GravityScript>().onGround){
            if(lookDir.y > 0){
                anim[0].transform.localScale = new Vector3(-s, s, s);
                anim[1].transform.localScale = new Vector3(-s, s, s);
            }else{
                anim[0].transform.localScale = new Vector3(s, s, s);
                anim[1].transform.localScale = new Vector3(s, s, s);
            }
        }
    }
}
