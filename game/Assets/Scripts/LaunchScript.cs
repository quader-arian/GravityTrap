using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchScript : MonoBehaviour
{
    private Rigidbody2D body;
    private Camera cam;
    private Vector2 mousePos;

    public float force = 20f;

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
       if(Input.GetButtonDown("Fire1")){
        Lauch();
       }
    }

    void Lauch(){
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 pos = transform.position;
        Vector2 lookDir = (mousePos - pos);

        //float rotZ = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(0, 0, rotZ);
        body.AddForce(lookDir.normalized * force, ForceMode2D.Impulse);
    }
}
