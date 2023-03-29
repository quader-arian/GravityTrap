using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimScript1 : MonoBehaviour
{
    private Camera cam;
    private Vector3 mousePos;
    
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        GameObject player = GameObject.Find("Player");
        Vector3 lookDir = (player.transform.position - transform.position);

        float rotZ = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }
}
