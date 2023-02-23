using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityScript : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Up"){
            Physics2D.gravity = new Vector2(0, 9.8f);
        }else if(col.gameObject.tag == "Left"){
            Physics2D.gravity = new Vector2(-9.8f, 0);
        }else if(col.gameObject.tag == "Right"){
            Physics2D.gravity = new Vector2(9.8f, 0);
        }else{
            Physics2D.gravity = new Vector2(0, -9.8f);
        }
    }
}
