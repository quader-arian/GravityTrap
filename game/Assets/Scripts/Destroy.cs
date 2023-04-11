using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public float timer = 0f;
    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, timer);
    }
}
