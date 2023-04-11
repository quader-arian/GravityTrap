using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform target;
    public bool isFollowing = false;
    public float smoothSpeed = 0.15f;
    public float offsetNum = 2f;
    private Vector3 offset = new Vector3();
    public int shakeSamples = 10;
    private bool isShaking = false;
    private List<Vector2> precomputedShakeSamples;

    private void Start()
    {
        //offset = new Vector3(offset.x, offset.y, -10f); // Set the z offset to -10
        precomputedShakeSamples = PrecomputeShakeSamples(shakeSamples);
    }

    void FixedUpdate()
    {
        if (isFollowing && !isShaking)
        {
            target = GameObject.FindWithTag("Player").transform;
            if(Physics2D.gravity == new Vector2(0, 9.8f) && GameObject.FindWithTag("Player").GetComponent<GravityScript>().onGround){
                offset = new Vector3(0, -offsetNum, -10);
            }else if(Physics2D.gravity == new Vector2(0, -9.8f) && GameObject.FindWithTag("Player").GetComponent<GravityScript>().onGround){
                offset = new Vector3(0, offsetNum, -10);
            }else if(Physics2D.gravity == new Vector2(9.8f, 0) && GameObject.FindWithTag("Player").GetComponent<GravityScript>().onGround){
                offset = new Vector3(-offsetNum, 0, -10);
            }else if(Physics2D.gravity == new Vector2(-9.8f, 0) && GameObject.FindWithTag("Player").GetComponent<GravityScript>().onGround){
                offset = new Vector3(offsetNum, 0, -10);
            }else{
                offset = new Vector3(0, 0, -10);
            }
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }

    public void StartShake(float duration, float magnitude)
    {
        StartCoroutine(Shake(duration, magnitude));
    }

    private List<Vector2> PrecomputeShakeSamples(int sampleCount)
    {
        List<Vector2> samples = new List<Vector2>();
        for (int i = 0; i < sampleCount; i++)
        {
            float x = Random.Range(-1f, 1f);
            float y = Random.Range(-1f, 1f);
            samples.Add(new Vector2(x, y));
        }
        return samples;
    }

    private IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPosition = transform.position;
        float elapsed = 0f;
        isShaking = true;
        float interval = duration / shakeSamples;

        while (elapsed < duration)
        {
            int index = Mathf.FloorToInt(elapsed / interval) % shakeSamples;
            Vector2 randomSample = precomputedShakeSamples[index] * magnitude;

            transform.position = new Vector3(originalPosition.x + randomSample.x, originalPosition.y + randomSample.y, transform.position.z);
            elapsed += Time.deltaTime;
            yield return new WaitForSeconds(interval);
        }

        isShaking = false;
        transform.position = originalPosition;
    }
}
