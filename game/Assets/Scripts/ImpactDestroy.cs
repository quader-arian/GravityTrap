using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactDestroy : MonoBehaviour
{
    public float timer;

    void OnTriggerEnter2D(Collider2D col){
        Destroy(gameObject);
    }

    void Start(){
        Destroy(gameObject, timer);
    }
}
