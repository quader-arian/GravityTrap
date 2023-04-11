using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WasdScript : MonoBehaviour
{
    //character movement speed
    public float speed = 5.0f;

    //while the character is colliding with a surface
    void OnCollisionStay2D(Collision2D col)
    {
        //current direction of gravity
        Vector2 g = Physics2D.gravity;

        //move up & down when on walls
        if (g.Equals(new Vector2(9.8f, 0f)) || g.Equals(new Vector2(-9.8f, 0f)))
        {
            if (Input.GetKey(KeyCode.W))
            {
                transform.position += Vector3.up * speed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.position += Vector3.down * speed * Time.deltaTime;
            }
        }

        //move left & right when on ceiling/ground
        if (g.Equals(new Vector2(0f, 9.8f)) || g.Equals(new Vector2(0f, -9.8f)))
        {
            if (Input.GetKey(KeyCode.A))
            {
                transform.position += Vector3.left * speed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.position += Vector3.right * speed * Time.deltaTime;
            }
        }
    }

}
