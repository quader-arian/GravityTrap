using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
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
            }else if(onAreaContactExplode){
                //
            }
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "AttackArea"){
            if(onAreaContactKill){
                Destroy(gameObject, 0.5f);
            }else if(onAreaContactExplode){
                //
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player"){
            if(onContactKill){
                Destroy(gameObject);
            }else if(onContactExplode){
                //
            }
        }
    }
    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player"){
            if(onContactKill){
                Destroy(gameObject);
            }else if(onContactExplode){
                //
            }
        }
    }
}
