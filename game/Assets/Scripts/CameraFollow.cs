using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private GameObject player;
    public float offset;
    public float offsetSmoothing;
    private Vector3 playerPosition;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
        Vector2 g = Physics2D.gravity;

        if(g.Equals(new Vector2(9.8f, 0f))){
            playerPosition = new Vector3(playerPosition.x - offset , playerPosition.y, playerPosition.z);
        }else if(g.Equals(new Vector2(-9.8f, 0f))){
            playerPosition = new Vector3(playerPosition.x + offset, playerPosition.y, playerPosition.z);
        }else if(g.Equals(new Vector2(0f, 9.8f))){
            playerPosition = new Vector3(playerPosition.x, playerPosition.y + offset, playerPosition.z);
        }else if(g.Equals(new Vector2(0f, -9.8f))){
            playerPosition = new Vector3(playerPosition.x, playerPosition.y - offset, playerPosition.z);
        }

        transform.position = Vector3.Lerp(transform.position, playerPosition, offsetSmoothing * Time.deltaTime);
    }
}