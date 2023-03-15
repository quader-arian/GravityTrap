using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawScript : MonoBehaviour
{
    private Camera cam;
    private bool waitForLine1 = false;
    private bool waitForLine2 = false;
    private bool intersect = false;
    public GameObject brush;
    public GameObject sensor;
    public GameObject allLines;
    private Rigidbody2D body;
    private Vector3[] allPoints;
    private LineRenderer currentLineRenderer;
    private GameObject currentLineObject;
    public Gradient gradient;

    private Vector2 lastPos;
    public bool onGround = false;
    public int tolerance;
    private int currTol;

    private PolygonCollider2D collideArea;
    private bool foundFirst;

    public CameraShake shake;

    void Start(){
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        body = GetComponent<Rigidbody2D>();
        CreateBrush();
        currTol = tolerance;
    }

    void Update(){
        if(!waitForLine1 && !waitForLine2){
            Vector2 mousePos = this.transform.position;
            float round = 100f;
            float mpRoundX = Mathf.Round(mousePos.x * round) / round;
            float mpRoundY = Mathf.Round(mousePos.y * round) / round;
            float lpRoundX = Mathf.Round(lastPos.x * round) / round;
            float lpRoundY = Mathf.Round(lastPos.y * round) / round;
            if(mpRoundX != lpRoundX && mpRoundY != lpRoundY){
                currTol--;
                if(currTol<=0){
                    AddAPoint(mousePos);
                    currTol = tolerance;
                }
                lastPos = mousePos;
            }
        }else if(GetComponentInParent<GravityScript>().onGround){
            waitForLine1 = false;
        }else if(GetComponentInParent<GravityScript>().onGroundExit && !waitForLine1){
            waitForLine2 = false;
            CreateBrush();
        }

        if(intersect){
            Debug.Log("Intersect");
            Destroy(currentLineObject, 0.5f);
            currentLineRenderer.colorGradient = gradient;
            currentLineRenderer = null;
            waitForLine1 = true;
            waitForLine2 = true;
            intersect = false;
            StartCoroutine(shake.Shake(.1f, .1f));
        }
    }

    void CreateBrush(){
        GameObject brushInstance = Instantiate(brush);
        currentLineObject = brushInstance;
        currentLineRenderer = brushInstance.GetComponent<LineRenderer>();
        currentLineRenderer.transform.parent = allLines.transform;

        Vector2 mousePos = this.transform.position;

        currentLineRenderer.SetPosition(0, mousePos);
        currentLineRenderer.SetPosition(1, mousePos);
        GameObject sensorInstance = Instantiate(sensor, mousePos, Quaternion.identity);
        sensorInstance.transform.parent = currentLineRenderer.transform;
    }

    void AddAPoint(Vector2 pointPos){
        currentLineRenderer.positionCount++;
        int positionIndex = currentLineRenderer.positionCount -1;
        currentLineRenderer.SetPosition(positionIndex, pointPos);
        GameObject sensorInstance = Instantiate(sensor, pointPos, Quaternion.identity);
        sensorInstance.transform.parent = currentLineRenderer.transform;
    }

    void OnTriggerStay2D(Collider2D col){
        if (col.gameObject.tag == "Sensor"){
            col.gameObject.GetComponent<SensorScript>().prevSensor = false;
        }
    }
    void OnTriggerExit2D(Collider2D col){
        if (col.gameObject.tag == "Sensor"){
            col.gameObject.GetComponent<SensorScript>().prevSensor = true;
        }
    }

    //when it enters for first time
    void OnTriggerEnter2D(Collider2D col){
        if (col.gameObject.tag == "Sensor" && col.gameObject.GetComponent<SensorScript>().prevSensor && !GetComponentInParent<GravityScript>().onGround && currentLineRenderer != null){
            Vector2 intersectPoint = col.transform.position;
            AddAPoint(intersectPoint);
            intersect = true;

            collideArea = currentLineObject.AddComponent<PolygonCollider2D>();
            collideArea.isTrigger = true;

            Vector3[] pos = new Vector3[currentLineRenderer.positionCount];
            int count = 0;
            foundFirst = false;
            currentLineRenderer.GetPositions(pos);
            foreach(Vector3 temp in pos){
                Vector2 p = temp;
                if(p == intersectPoint && !foundFirst){
                    foundFirst = true;
                    count++;
                }else if(foundFirst){
                    count++;
                }
            }
            int maxCount = count - 1;
            Vector2[] newPos = new Vector2[maxCount];
            count = 0;
            foundFirst = false;
            foreach(Vector3 temp in pos){
                Vector2 p = temp;
                if(p == intersectPoint && !foundFirst && count < maxCount){
                    foundFirst = true;
                    newPos[count] = p;
                    count++;
                }else if(foundFirst && count < maxCount){
                    newPos[count] = p;
                    count++;
                }
            }
            collideArea.SetPath(0, newPos);
        }
    }
}