using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityScript : MonoBehaviour
{
    public GameObject signal;

    void OnCollisionEnter2D(Collision2D col)
    {
        GravityChange(col);
    }

    void OnCollisionStay2D(Collision2D col)
    {
        GravityChange(col);
    }

    void GravityChange(Collision2D col){
        if (col.gameObject.tag == "Up"){
            Physics2D.gravity = new Vector2(0, 9.8f);
            signal.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if(col.gameObject.tag == "Left"){
            Physics2D.gravity = new Vector2(-9.8f, 0);
            signal.transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        if(col.gameObject.tag == "Right"){
            Physics2D.gravity = new Vector2(9.8f, 0);
            signal.transform.rotation = Quaternion.Euler(0, 0, 270);
        }
        if(col.gameObject.tag == "Down"){
            Physics2D.gravity = new Vector2(0, -9.8f);
            signal.transform.rotation = Quaternion.Euler(0, 0, 180);
        }
    }
}
