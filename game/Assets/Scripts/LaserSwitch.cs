using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float onTime = 4f;
    public float offTime = 4f;
    private float timer;
    private bool switcher = false;

    // Start is called before the first frame update
    void Start()
    {
        timer = offTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer < onTime && switcher){
            timer = offTime;
        }else if(timer < offTime && !switcher){
            timer = onTime;
        }
        timer -= Time.deltaTime;
    }
}
