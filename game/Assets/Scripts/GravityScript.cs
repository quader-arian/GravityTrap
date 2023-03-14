using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityScript : MonoBehaviour
{
    public CameraShake shake;
    public GameObject signal;
    public bool onGround = false;
    public bool onGroundExit = false;

    void OnCollisionEnter2D(Collision2D col)
    {
        GravityChange(col);
        StartCoroutine(shake.Shake(.04f, .04f));
        onGroundExit = false;
    }
    void OnCollisionStay2D(Collision2D col)
    {
        GravityChange(col);
        onGroundExit = false;
    }
    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Up" || col.gameObject.tag == "Left" || col.gameObject.tag == "Right" || col.gameObject.tag == "Down"){
            onGround = false;
            onGroundExit = true;
        }
        StartCoroutine(shake.Shake(.04f, .04f));
    }

    void GravityChange(Collision2D col){
        if (col.gameObject.tag == "Up"){
            Physics2D.gravity = new Vector2(0, 9.8f);
            signal.transform.rotation = Quaternion.Euler(0, 0, 0);
            onGround = true;
        }
        if(col.gameObject.tag == "Left"){
            Physics2D.gravity = new Vector2(-9.8f, 0);
            signal.transform.rotation = Quaternion.Euler(0, 0, 90);
            onGround = true;
        }
        if(col.gameObject.tag == "Right"){
            Physics2D.gravity = new Vector2(9.8f, 0);
            signal.transform.rotation = Quaternion.Euler(0, 0, 270);
            onGround = true;
        }
        if(col.gameObject.tag == "Down"){
            Physics2D.gravity = new Vector2(0, -9.8f);
            signal.transform.rotation = Quaternion.Euler(0, 0, 180);
            onGround = true;
        }
    }
}
