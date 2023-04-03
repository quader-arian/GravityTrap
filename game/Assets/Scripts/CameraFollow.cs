using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private GameObject player;
    public float offset;
    private Vector3 playerPosition;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 g = Physics2D.gravity;

        if(g.Equals(new Vector2(9.8f, 0f))){
            playerPosition = new Vector3(player.transform.position.x - offset , player.transform.position.y, transform.position.z);
        }else if(g.Equals(new Vector2(-9.8f, 0f))){
            playerPosition = new Vector3(player.transform.position.x + offset, player.transform.position.y, transform.position.z);
        }else if(g.Equals(new Vector2(0f, 9.8f))){
            playerPosition = new Vector3(player.transform.position.x, player.transform.position.y + offset, transform.position.z);
        }else if(g.Equals(new Vector2(0f, -9.8f))){
            playerPosition = new Vector3(player.transform.position.x, player.transform.position.y - offset, transform.position.z);
        }

        transform.position = playerPosition;
    }
}