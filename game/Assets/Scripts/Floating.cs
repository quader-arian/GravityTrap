using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floating : MonoBehaviour
{
    public float moveSpeed = 0.2f;
    public float offset = 0.1f;
    private Vector3[] movePoints = new Vector3[2];
    public int currentMovePoint = 0;

    // Start is called before the first frame update
    void Start()
    {
        movePoints[0] = new Vector3(transform.position.x, transform.position.y + offset, transform.position.z);
        movePoints[1] = new Vector3(transform.position.x, transform.position.y - offset, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position == movePoints[currentMovePoint]){
            currentMovePoint++;
            if(currentMovePoint >= movePoints.Length){
                currentMovePoint = 0;
            }
        }
        float step = moveSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, movePoints[currentMovePoint], step);
    }
}
