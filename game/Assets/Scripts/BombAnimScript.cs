using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombAnimScript : MonoBehaviour
{
    public GameObject idle;
    public GameObject mad;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player" && !GetComponentInParent<EnemyScript>().dying && !GetComponentInParent<EnemyScript>().restarting){
            idle.SetActive(false);
            mad.SetActive(true);
            mad.GetComponent<Animator>().Play("Entry", 0, 1);
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player" && !GetComponentInParent<EnemyScript>().dying && !GetComponentInParent<EnemyScript>().restarting){
            idle.SetActive(true);
            mad.SetActive(false);
            idle.GetComponent<Animator>().Play("Entry", 0, 1);
        }
    }
}
