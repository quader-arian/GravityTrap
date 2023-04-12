using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombAnimScript : MonoBehaviour
{
    public GameObject idle;
    public GameObject inflate;
    public GameObject deflate;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player" && !GetComponentInParent<EnemyScript>().dying && !GetComponentInParent<EnemyScript>().restarting){
            idle.SetActive(false);
            inflate.SetActive(true);
            deflate.SetActive(false);
            inflate.GetComponent<Animator>().Play("Entry", 0, 1);
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player" && !GetComponentInParent<EnemyScript>().dying && !GetComponentInParent<EnemyScript>().restarting){
            deflate.SetActive(true);
            inflate.SetActive(false);
            idle.SetActive(false);
            deflate.GetComponent<Animator>().Play("Entry", 0, 1);
        }
    }

    void Update(){
        if((deflate.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !deflate.GetComponent<Animator>().IsInTransition(0)) && !GetComponentInParent<EnemyScript>().dying && !GetComponentInParent<EnemyScript>().restarting){
            idle.SetActive(true);
            inflate.SetActive(false);
            deflate.SetActive(false);
            idle.GetComponent<Animator>().Play("Entry", 0, 1);
        }
    }
}
